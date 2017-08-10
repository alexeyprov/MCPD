//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace ServiceModelEx
{
    public class EventRelayHost
    {
        #region Private Fields

        //For serivce cert lookup
        StoreLocation _serviceCertLocation;
        StoreName _serviceCertStoreName;
        X509FindType _serviceCertFindType;
        object _serviceCertFindValue;

        //For message security
        string _applicationName;
        bool _useProviders;
        bool _anonymous;

        //Managing the host-per-event
        private readonly IDictionary<Type, IDictionary<string, ServiceBusHost>> _hosts;
        Type _serviceType;
        string[] _baseAddresses;
        object _singletonInstance;
        NetEventRelayBinding _binding;

        //Service bus credentials
        string _serviceBusPassword;
        StoreLocation _serviceBusCertLocation;
        StoreName _serviceBusCertStoreName;
        X509FindType _serviceBusCertFindType;
        object _serviceBusCertFindValue; 

        #endregion

        #region Constructors

        public EventRelayHost(object singletonInstance, string baseAddress) : this(singletonInstance, new string[] { baseAddress })
        {
        }

        public EventRelayHost(object singletonInstance, string[] baseAddresses) : this(baseAddresses)
        {
            _singletonInstance = singletonInstance;
        }

        public EventRelayHost(Type serviceType, string baseAddress) : this(serviceType, new string[] { baseAddress })
        {
        }

        public EventRelayHost(Type serviceType, string[] baseAddresses) : this(baseAddresses)
        {
            _serviceType = serviceType;
        }

        private EventRelayHost(string[] baseAddresses)
        {
            Debug.Assert(baseAddresses != null);
            Debug.Assert(baseAddresses.Length > 0);

            for (int index = 0; index < baseAddresses.Length; index++)
            {
                if (!baseAddresses[index].EndsWith("/"))
                {
                    baseAddresses[index] += "/";
                }
            }
            _baseAddresses = baseAddresses;

            _hosts = new Dictionary<Type, IDictionary<string, ServiceBusHost>>();

            //Try to guess a certificate 
            ConfigureAnonymousMessageSecurity(ServiceBusHelper.ExtractSolution(new Uri(baseAddresses[0])));
        }

        #endregion

        public void SetServiceBusCertificate(object findValue, StoreLocation location, StoreName storeName, X509FindType findType)
        {
            _serviceBusCertFindValue = findValue;
            _serviceBusCertLocation = location;
            _serviceBusCertStoreName = storeName;
            _serviceBusCertFindType = findType;
        }

        public void SetServiceBusCertificate(string subjectName)
        {
            SetServiceBusCertificate(subjectName, StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName);
        }
        public void SetServiceBusPassword(string password)
        {
            _serviceBusPassword = password;
        }

        public void ConfigureAnonymousMessageSecurity(string serviceCert)
        {
            ConfigureAnonymousMessageSecurity(serviceCert, StoreLocation.LocalMachine, StoreName.My);
        }
        public void ConfigureAnonymousMessageSecurity(string serviceCert, StoreLocation location, StoreName storeName)
        {
            ConfigureAnonymousMessageSecurity(location, storeName, X509FindType.FindBySubjectName, serviceCert);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ConfigureAnonymousMessageSecurity(StoreLocation location, StoreName storeName, X509FindType findType, object findValue)
        {
            _serviceCertLocation = location;
            _serviceCertStoreName = storeName;
            _serviceCertFindType = findType;
            _serviceCertFindValue = findValue;
            _anonymous = true;
        }

        public void ConfigureMessageSecurity(string serviceCert)
        {
            ConfigureMessageSecurity(serviceCert, StoreLocation.LocalMachine, StoreName.My, true, null);
        }
        public void ConfigureMessageSecurity(string serviceCert, string applicationName)
        {
            ConfigureMessageSecurity(serviceCert, StoreLocation.LocalMachine, StoreName.My, true, applicationName);
        }
        public void ConfigureMessageSecurity(string serviceCert, bool useProviders, string applicationName)
        {
            ConfigureMessageSecurity(serviceCert, StoreLocation.LocalMachine, StoreName.My, useProviders, applicationName);
        }
        public void ConfigureMessageSecurity(string serviceCert, StoreLocation location, StoreName storeName, bool useProviders, string applicationName)
        {
            ConfigureMessageSecurity(location, storeName, X509FindType.FindBySubjectName, serviceCert, useProviders, applicationName);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        void ConfigureMessageSecurity(StoreLocation location, StoreName storeName, X509FindType findType, object findValue, bool useProviders, string applicationName)
        {
            _serviceCertLocation = location;
            _serviceCertStoreName = storeName;
            _serviceCertFindType = findType;
            _serviceCertFindValue = findValue;
            _useProviders = useProviders;
            _applicationName = applicationName;
            _anonymous = false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetBinding(NetEventRelayBinding binding)
        {
            _binding = binding;
        }

        public void SetBinding(string bindingConfigName)
        {
            SetBinding(new NetEventRelayBinding(bindingConfigName));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Subscribe()
        {
            Type serviceType = _serviceType ?? _singletonInstance.GetType();

            foreach (Type interfaceType in serviceType.GetInterfaces())
            {
                if (interfaceType.GetCustomAttributes(typeof(ServiceContractAttribute), false).Length == 1)
                {
                    Subscribe(interfaceType);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Subscribe(Type contractType)
        {
            string[] operations = GetOperations(contractType);

            foreach (string operationName in operations)
            {
                Subscribe(contractType, operationName);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Subscribe(Type contractType, string operation)
        {
            Debug.Assert(!string.IsNullOrEmpty(operation));

            IDictionary<string, ServiceBusHost> hostsByOperation = null;

            if (!_hosts.TryGetValue(contractType, out hostsByOperation))
            {
                hostsByOperation = GetOperations(contractType).ToDictionary(s => s, s => (ServiceBusHost)null);
                _hosts[contractType] = hostsByOperation;
            }
            if (hostsByOperation[operation] == null)
            {
                IEnumerable<Uri> baseAddressesList =
                    from a in _baseAddresses
                    select new Uri(a + contractType);

                ServiceBusHost host = (_serviceType != null) ?
                    new ServiceBusHost(_serviceType, baseAddressesList.ToArray()) :
                    new ServiceBusHost(_singletonInstance, baseAddressesList.ToArray());

                hostsByOperation[operation] = host;

                host.AddServiceEndpoint(contractType, _binding ?? new NetEventRelayBinding(), operation);

                //Configure service bus credentials for the host
                if (_serviceBusPassword != null)
                {
                    host.SetServiceBusPassword(_serviceBusPassword);
                }
                if (_serviceBusCertFindValue != null)
                {
                    host.SetServiceBusCertificate(_serviceBusCertFindValue, _serviceBusCertLocation, _serviceBusCertStoreName, _serviceBusCertFindType);
                }

                //Configure message security
                if (_anonymous)
                {
                    host.ConfigureAnonymousMessageSecurity(_serviceCertLocation, _serviceCertStoreName, _serviceCertFindType, _serviceCertFindValue);
                }
                else
                {
                    host.ConfigureMessageSecurity(_serviceCertLocation, _serviceCertStoreName, _serviceCertFindType, _serviceCertFindValue, _useProviders, _applicationName);
                }

                host.Open();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Unsubscribe()
        {
            foreach (Type contractType in _hosts.Keys)
            {
                Unsubscribe(contractType);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Unsubscribe(Type contractType)
        {
            string[] operations = GetOperations(contractType);

            foreach (string operationName in operations)
            {
                Unsubscribe(contractType, operationName);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Unsubscribe(Type contractType, string operation)
        {
            Debug.Assert(string.IsNullOrEmpty(operation) == false);

            if (_hosts.ContainsKey(contractType) == false)
            {
                return;
            }
            if (_hosts[contractType][operation] != null)
            {
                _hosts[contractType][operation].Close();
                _hosts[contractType][operation] = null;
            }
        }

        private static string[] GetOperations(Type contract)
        {
            return contract.GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance)
                .Select(m => m.Name)
                .Distinct()
                .ToArray();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Abort()
        {
            foreach (ServiceHost host in _hosts.Values
                .SelectMany(d => d.Values)
                .Where(h => h != null))
            {
                host.Abort();
            }
        }
    }
}
