///-------------------------------------------------------------------------------------------------------------
/// <summary>
///
///     WebProfileGenerator - Generates a strong type accessor for site profile from web.config
///
/// </summary>
///-------------------------------------------------------------------------------------------------------------
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Designer.Interfaces;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Design;
using Microsoft.VisualStudio.Shell.Design.Serialization;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.UI.Design;
using IServiceProvider = System.IServiceProvider;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace WebProfileGenerator
{
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    internal class WebProfileGenerator 
    {
        private EnvDTE.ProjectItem _webConfig;
        private string             _configFullPath;
        private string             _configFolder;
        private ServiceProvider    _globalServiceProvider;
        private ServiceProvider    _itemServiceProvider;
        private IVsHierarchy       _hierarchy;
        private uint               _configItemID;
        private string             _codeExtension;
        private string             _namespace;
        private string             _className = "WebProfile";

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Constructor 
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private WebProfileGenerator()
        {
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Static entrypoint to generate the WebProfile for the given web.config.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        public static void Generate(EnvDTE.ProjectItem webConfig)
        {
            // Verify inputs
            if (webConfig == null)
            {
                throw new ArgumentNullException("webConfig");
            }

            (new WebProfileGenerator()).GenerateWebProfile(webConfig);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///    Generate the WebProfile for the given web.config.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private void GenerateWebProfile(EnvDTE.ProjectItem webConfig)
        {
            // Initilize member state
            Initialize(webConfig);

            // Generate the code
            string code = GenerateCode();

            // Save the WebProfile code
            SaveWebProfile(code);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///    Initilize member state
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private void Initialize(EnvDTE.ProjectItem webConfig)
        {
            // Save web config project item
            _webConfig = webConfig;

            // Get the full path to web config
            _configFullPath = webConfig.get_FileNames(1);
            if (string.IsNullOrEmpty(_configFullPath))
            {
                throw new WebProfileGeneratorException("Failed to get full path to web.config.");
            }

            // Get the disk folder for web config
            _configFolder = Path.GetDirectoryName(_configFullPath);
            if (string.IsNullOrEmpty(_configFolder))
            {
                throw new WebProfileGeneratorException("Failed to get web.config folder.");
            }

            // Get the VS global service provider from DTE
            IOleServiceProvider globalServiceProvider = webConfig.DTE as IOleServiceProvider;
            if (globalServiceProvider != null)
            {
                _globalServiceProvider = new ServiceProvider(globalServiceProvider);
            }
            if (_globalServiceProvider == null)
            {
                throw new WebProfileGeneratorException("Failed to get VS global service provider.");
            }

            // Get the hierarchy and ItemID of web.config
            GetHierarchyAndItemID(webConfig, out _hierarchy, out _configItemID);
            if (_hierarchy == null || _configItemID == 0)
            {
                throw new WebProfileGeneratorException("Failed to get herarchy and itemid of web.config.");
            }

            // Get the item service provider for web.config
            IVsProject vsProject = _hierarchy as IVsProject;
            if (vsProject != null)
            {
                IOleServiceProvider itemServiceProvider;
                vsProject.GetItemContext(_configItemID, out itemServiceProvider);
                if (itemServiceProvider != null)
                {
                    _itemServiceProvider = new ServiceProvider(itemServiceProvider);
                }
            }
            if (_itemServiceProvider == null)
            {
                throw new WebProfileGeneratorException("Failed to get item service provider for web.config.");
            }

            // Get the namespace to use
            EnvDTE.Project project = webConfig.ContainingProject;
            if (project != null)
            {
                bool isVB = false;
                EnvDTE.CodeModel codeModel = project.CodeModel;
                if (codeModel != null && codeModel.Language == EnvDTE.CodeModelLanguageConstants.vsCMLanguageVB)
                {
                    isVB = true;
                }

                if (!isVB)
                {
                    EnvDTE.Properties properties = project.Properties;
                    if (properties != null)
                    {
                        EnvDTE.Property property = properties.Item("RootNamespace");
                        if (property != null)
                        {
                            string rootNamespace = property.Value as string;
                            if (!string.IsNullOrEmpty(rootNamespace))
                            {
                                _namespace = rootNamespace;
                            }
                        }
                    }
                }
            }

            // Get the code file extension
            CodeDomProvider codeProvider = CodeProvider;
            if (CodeProvider != null)
            {
                _codeExtension = codeProvider.FileExtension;
                if (!_codeExtension.StartsWith("."))
                {
                    _codeExtension = "." + _codeExtension;
                }
            }
            if (string.IsNullOrEmpty(_codeExtension))
            {
                throw new WebProfileGeneratorException("failed to get code dom provider code file extension");
            }
        }

        private void SaveWebProfile(string code)
        {
            // Locate WebProfile file in project
            string fileName = _className + _codeExtension;
            EnvDTE.ProjectItem webProfile = null;
            foreach (EnvDTE.ProjectItem item in _webConfig.Collection)
            {
                string itemName = item.Name;
                if (itemName != null && itemName.Length == fileName.Length
                    && string.Compare(itemName, fileName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    webProfile = item;
                    break;
                }
            }

            if (webProfile != null)
            {
                using (DocData docData = new EditDocData(_globalServiceProvider, webProfile.get_FileNames(1)))
                {
                    // Try to check out file (this throws)
                    docData.CheckoutFile(_globalServiceProvider);

                    // Write out the new code
                    using (DocDataTextWriter writer = new DocDataTextWriter(docData))
                    {
                        writer.Write(code);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            else
            {
                // Write the code to disk
                string fullName = Path.Combine(_configFolder, fileName);
                using (StreamWriter sw = File.CreateText(fullName))
                {
                    sw.Write(code);
                }

                // Add the disk file to the project
                _webConfig.Collection.AddFromFileCopy(fullName);
            }
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Calls to generate CodeCompile unit and then converts code dom
        ///     to string using CodeDomProvider.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private string GenerateCode()
        {
            // Generate the profile code dom tree
            CodeCompileUnit codeCompileUnit = GenerateCodeCompileUnit();

            // Convert code dom tree to code in current CodeProvider language
            string code = null;
            using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
            {
                CodeDomProvider codeProvider = CodeProvider;
                codeProvider.GenerateCodeFromCompileUnit(codeCompileUnit, sw, null);
                code = sw.ToString();
            }

            // Return code as string
            return code;
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the CodeDomProvider for the curent project.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        public CodeDomProvider CodeProvider
        {
            get
            {
                IVSMDCodeDomProvider vsmdCodeDomProvider = GetItemService<IVSMDCodeDomProvider>();
                if (vsmdCodeDomProvider != null)
                {
                    return vsmdCodeDomProvider.CodeDomProvider as CodeDomProvider;
                }
                return null;
            }
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the ITypeResolutionService for the curent project.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        public ITypeResolutionService TypeResolutionService
        {
            get
            {
                if (_hierarchy != null)
                {
                    DynamicTypeService dynamicTypeService = GetGlobalService<DynamicTypeService>();
                    if (dynamicTypeService != null)
                    {
                        return dynamicTypeService.GetTypeResolutionService(_hierarchy);
                    }
                }
                return null;
            }
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets a service from the VS global service provider.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private InterfaceType GetGlobalService<InterfaceType>() where InterfaceType : class
        {
            return GetService<InterfaceType>(_globalServiceProvider);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets a service from the project item service provider.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private InterfaceType GetItemService<InterfaceType>() where InterfaceType : class
        {
            return GetService<InterfaceType>(_itemServiceProvider);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets a service from the provided service provider.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static InterfaceType GetService<InterfaceType>(IServiceProvider serviceProvider) where InterfaceType : class
        {
            InterfaceType service = null;
            try
            {
                if (serviceProvider != null)
                {
                    service = serviceProvider.GetService(typeof(InterfaceType)) as InterfaceType;
                }
            }
            catch (Exception)
            {
            }
            return service;
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets the VS hierarchy and itemid for the provided project item
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private void GetHierarchyAndItemID(EnvDTE.ProjectItem projectItem, out IVsHierarchy hierarchy, out uint itemid)
        {
            hierarchy = null;
            itemid = 0;
            if (projectItem != null)
            {
                string fullPath = projectItem.get_FileNames(1);
                if (!string.IsNullOrEmpty(fullPath))
                {
                    EnvDTE.Project project = projectItem.ContainingProject;
                    if (project != null)
                    {
                        IVsSolution vsSolution = GetGlobalService<IVsSolution>();
                        if (vsSolution != null)
                        {
                            IVsHierarchy vsHierarchy = null;
                            vsSolution.GetProjectOfUniqueName(project.UniqueName, out vsHierarchy);
                            IVsProject vsProject = vsHierarchy as IVsProject;
                            if (vsProject != null)
                            {
                                int isFound = 0;
                                uint vsItemid = VSConstants.VSITEMID_NIL;
                                VSDOCUMENTPRIORITY[] priority = new VSDOCUMENTPRIORITY[1];
                                vsProject.IsDocumentInProject(fullPath, out isFound, priority, out vsItemid);
                                if (isFound != 0 && vsItemid != VSConstants.VSITEMID_NIL)
                                {
                                    itemid = vsItemid;
                                    hierarchy = vsHierarchy;
                                }
                            }
                        }
                    }
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Helper to get config access
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        ProfileSection GetProfileConfig()
        {
            // If config file is opened and dirty we save it
            // to ensure the load from disk has the latest changes
            if (_webConfig.IsDirty)
            {
                _webConfig.Save(null);
            }

            WebConfigurationFileMap webMap = new WebConfigurationFileMap();
            VirtualDirectoryMapping vDirMap = new VirtualDirectoryMapping(_configFolder, true);
            webMap.VirtualDirectories.Add("/", vDirMap);

            Configuration config = WebConfigurationManager.OpenMappedWebConfiguration(webMap, "/");
            if (config != null)
            {
                ProfileSection profile = (ProfileSection)config.GetSection("system.web/profile");
                if (profile != null)
                {
                    return profile;
                }
            }

            return null;
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Collects property data from config
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private Hashtable GetPropertiesForCompilation(ProfileSection config)
        {
            Hashtable ht = new Hashtable();
            if (config.PropertySettings != null)
            {
                AddProfilePropertySettingsForCompilation(config.PropertySettings, ht, null);

                foreach (ProfileGroupSettings pgs in config.PropertySettings.GroupSettings)
                {
                    AddProfilePropertySettingsForCompilation(pgs.PropertySettings, ht, pgs.Name);
                }
            }
            return ht;
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Collects property data from config collection
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private void AddProfilePropertySettingsForCompilation(ProfilePropertySettingsCollection propertyCollection, Hashtable ht, string groupName)
        {
            foreach (ProfilePropertySettings pps in propertyCollection)
            {
                ProfileNameTypeStruct prop = new ProfileNameTypeStruct();
                if (groupName != null)
                {
                    prop.Name = groupName + "." + pps.Name;
                }
                else
                {
                    prop.Name = pps.Name;
                }

                Type t = null;

                // First, try to resolve simple types
                if (t == null)
                {
                    t = ResolvePropertyTypeForCommonTypes(pps.Type.ToLower(System.Globalization.CultureInfo.InvariantCulture));
                }

                // Second, try to resolve using project references
                if (t == null)
                {
                    t = TypeResolutionService.GetType(pps.Type, false);
                }

                if (t != null)
                {
                    // Reference type
                    prop.PropertyCodeRefType = new CodeTypeReference(t);
                }
                else
                {
                    // Reference type name
                    string typeName = pps.Type;
                    if (!string.IsNullOrEmpty(typeName))
                    {
                        int firstComma = typeName.IndexOf(',');
                        if (firstComma > 0)
                        {
                            // Strip off assembly info
                            typeName = typeName.Substring(0, firstComma).Trim();
                        }
                        else
                        {
                            typeName = typeName.Trim();
                        }
                    }
                    prop.PropertyCodeRefType = new CodeTypeReference(typeName);
                }

                //prop.PropertyType = t;
                prop.IsReadOnly = pps.ReadOnly;
                //prop.LineNumber = pps.ElementInformation.Properties["name"].LineNumber;
                //prop.FileName = pps.ElementInformation.Properties["name"].Source;
                ht.Add(prop.Name, prop);
            }
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Helper to map config type names to actual Type
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        static private Type ResolvePropertyTypeForCommonTypes(string typeName)
        {
            switch (typeName)
            {
                case "string":
                    return typeof(string);

                case "byte":
                case "int8":
                    return typeof(byte);

                case "boolean":
                case "bool":
                    return typeof(bool);

                case "char":
                    return typeof(char);

                case "int":
                case "integer":
                case "int32":
                    return typeof(int);

                case "date":
                case "datetime":
                    return typeof(DateTime);

                case "decimal":
                    return typeof(decimal);

                case "double":
                case "float64":
                    return typeof(System.Double);

                case "float":
                case "float32":
                    return typeof(float);

                case "long":
                case "int64":
                    return typeof(long);

                case "short":
                case "int16":
                    return typeof(System.Int16);

                case "single":
                    return typeof(Single);

                case "uint16":
                case "ushort":
                    return typeof(UInt16);

                case "uint32":
                case "uint":
                    return typeof(uint);

                case "ulong":
                case "uint64":
                    return typeof(ulong);

                case "object":
                    return typeof(object);

                default:
                    return null;
            }
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Generate the code compile unit
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        internal CodeCompileUnit GenerateCodeCompileUnit()
        {
            ProfileSection config = GetProfileConfig();

            Hashtable properties = GetPropertiesForCompilation(config);
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            Hashtable groups = new Hashtable();

            CodeNamespace ns = new CodeNamespace();
            ns.Name = _namespace;

            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Web"));
            ns.Imports.Add(new CodeNamespaceImport("System.Web.Profile"));
            ns.Imports.Add(new CodeNamespaceImport("System.Configuration"));

            CodeTypeDeclaration type = new CodeTypeDeclaration();
            type.Name = _className;

            foreach (DictionaryEntry de in properties)
            {
                ProfileNameTypeStruct property = (ProfileNameTypeStruct)de.Value;

                int pos = property.Name.IndexOf('.');
                if (pos < 0)
                {
                    // public string Color { get { return (string) GetProperty("Color"); } set { SetProperty("Color", value); } }
                    CreateCodeForProperty(type, property);
                }
                else
                {
                    string grpName = property.Name.Substring(0, pos);
                    if (groups[grpName] == null)
                    {
                        groups.Add(grpName, property.Name);
                    }
                    else
                    {
                        groups[grpName] = ((string)groups[grpName]) + ";" + property.Name;
                    }
                }
            }

            foreach (DictionaryEntry de in groups)
            {
                // public ProfileGroupFooClass Foo { get { return ProfileGroupSomething; }}
                //
                // public class ProfileGroupFoo : ProfileGroup {
                //      Properties
                // }
                AddPropertyGroup((string)de.Key, (string)de.Value, properties, type, ns);
            }

            // public ASP.Profile GetProfileForUser(string username) {
            //      return (ASP.Profile) this.GetUserProfile(username);
            // }
            AddCodeForGetProfileForUser(type);

            // Add other members
            AddProfileBaseDelegators(type);

            // }
            //
            ns.Types.Add(type);
            compileUnit.Namespaces.Add(ns);

            return compileUnit;
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates standard methods of ProfileBase that delegate to base
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddProfileBaseDelegators(CodeTypeDeclaration ctd)
        {
            // Add field
            string fieldName = "_profileBase";
            ctd.Members.Add(new CodeMemberField(typeof(ProfileBase), fieldName));

            // Add default constructor
            CodeConstructor ctor = new CodeConstructor();
            ctor.Attributes = MemberAttributes.Public;
            ctor.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodeObjectCreateExpression(typeof(ProfileBase), new CodeExpression[] { })));
            ctd.Members.Add(ctor);

            // Add proxy constructor
            ctor = new CodeConstructor();
            ctor.Attributes = MemberAttributes.Public;
            ctor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(ProfileBase)), "profileBase"));
            ctor.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodeVariableReferenceExpression("profileBase")));
            ctd.Members.Add(ctor);

            // Add Current accessor
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            prop.HasGet = true;
            prop.Name = "Current";
            prop.Type = new CodeTypeReference(ctd.Name);
            prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeObjectCreateExpression(ctd.Name, new CodeExpression[] { new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(System.Web.HttpContext)), "Current"), "Profile") })));
            ctd.Members.Add(prop);

            // Add ProfileBase accessor
            prop = new CodeMemberProperty();
            prop.Attributes = MemberAttributes.Public;
            prop.HasGet = true;
            prop.Name = "ProfileBase";
            prop.Type = new CodeTypeReference(typeof(ProfileBase));
            prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));
            ctd.Members.Add(prop);

            // Add ProfileBase delegators
            AddDelegatingIndexedProperty(ctd, fieldName, typeof(object), typeof(string), "propertyName");
            AddDelegatingMethod(ctd, fieldName, "GetPropertyValue", typeof(object), new Type[] { typeof(string) }, new string[] { "propertyName" });
            AddDelegatingMethod(ctd, fieldName, "SetPropertyValue", null, new Type[] { typeof(string), typeof(object) }, new string[] { "propertyName", "propertyValue" });
            AddDelegatingMethod(ctd, fieldName, "GetProfileGroup", typeof(ProfileGroupBase), new Type[] { typeof(string) }, new string[] { "groupName" });
            AddDelegatingMethod(ctd, fieldName, "Initialize", null, new Type[] { typeof(string), typeof(bool) }, new string[] { "username", "isAuthenticated" });
            AddDelegatingMethod(ctd, fieldName, "Save", null, null, null);
            AddDelegatingMethod(ctd, fieldName, "Initialize", null, new Type[] { typeof(SettingsContext), typeof(SettingsPropertyCollection), typeof(SettingsProviderCollection) }, new string[] { "context", "properties", "providers" });
            AddStaticDelegatingMethod(ctd, typeof(ProfileBase), "Synchronized", typeof(SettingsBase), new Type[] { typeof(SettingsBase) }, new string[] { "settingsBase" });
            AddStaticDelegatingMethod(ctd, typeof(ProfileBase), "Create", typeof(ProfileBase), new Type[] { typeof(string) }, new string[] { "userName" });
            AddStaticDelegatingMethod(ctd, typeof(ProfileBase), "Create", typeof(ProfileBase), new Type[] { typeof(string), typeof(bool) }, new string[] { "userName", "isAuthenticated" });
            AddDelegatingReadOnlyProperty(ctd, fieldName, "UserName", typeof(string));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "IsAnonymous", typeof(bool));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "IsDirty", typeof(bool));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "LastActivityDate", typeof(DateTime));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "LastUpdatedDate", typeof(DateTime));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "Providers", typeof(SettingsProviderCollection));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "PropertyValues", typeof(SettingsPropertyValueCollection));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "Context", typeof(SettingsContext));
            AddDelegatingReadOnlyProperty(ctd, fieldName, "IsSynchronized", typeof(bool));
            AddStaticDelegatingReadOnlyProperty(ctd, typeof(ProfileBase), "Properties", typeof(SettingsPropertyCollection));
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates standard methods of ProfileGroupBase that delegate to base
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddProfileGroupBaseDelegators(CodeTypeDeclaration ctd)
        {
            // Add field
            string fieldName = "_profileGroupBase";
            ctd.Members.Add(new CodeMemberField(typeof(ProfileGroupBase), fieldName));

            // Add default constructor
            CodeConstructor ctor = new CodeConstructor();
            ctor.Attributes = MemberAttributes.Public;
            ctor.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodeObjectCreateExpression(new CodeTypeReference(typeof(ProfileGroupBase)), new CodeExpression[] { })));
            ctd.Members.Add(ctor);

            // Add proxy constructor
            ctor = new CodeConstructor();
            ctor.Attributes = MemberAttributes.Public;
            ctor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(ProfileGroupBase)), "profileGroupBase"));
            ctor.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodeVariableReferenceExpression("profileGroupBase")));
            ctd.Members.Add(ctor);

            // Add ProfileGroupBase accessor
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Attributes = MemberAttributes.Public;
            prop.HasGet = true;
            prop.Name = "ProfileGroupBase";
            prop.Type = new CodeTypeReference(typeof(ProfileGroupBase));
            prop.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));
            ctd.Members.Add(prop);

            // Add ProfileGroupBase delegators
            AddDelegatingIndexedProperty(ctd, fieldName, typeof(object), typeof(string), "propertyName");
            AddDelegatingMethod(ctd, fieldName, "GetPropertyValue", typeof(object), new Type[] { typeof(string) }, new string[] { "propertyName" });
            AddDelegatingMethod(ctd, fieldName, "SetPropertyValue", null, new Type[] { typeof(string), typeof(object) }, new string[] { "propertyName", "propertyValue" });
            AddDelegatingMethod(ctd, fieldName, "Init", null, new Type[] { typeof(ProfileBase), typeof(string) }, new string[] { "parent", "myName" });
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates a method that delegates to the base
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddDelegatingMethod(CodeTypeDeclaration ctd, string fieldName, string name, Type returnType, Type[] paramTypes, string[] paramNames)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = name;
            method.Attributes = MemberAttributes.Public;

            CodeMethodInvokeExpression cmie = new CodeMethodInvokeExpression();
            cmie.Method.TargetObject = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName);
            cmie.Method.MethodName = name;

            if (paramTypes != null)
            {
                for (int i = 0; i < paramTypes.Length; i++)
                {
                    method.Parameters.Add(new CodeParameterDeclarationExpression(paramTypes[i], paramNames[i]));
                    cmie.Parameters.Add(new CodeArgumentReferenceExpression(paramNames[i]));
                }
            }

            if (returnType != null)
            {
                method.ReturnType = new CodeTypeReference(returnType);
                CodeMethodReturnStatement ret = new CodeMethodReturnStatement(cmie);
                method.Statements.Add(ret);
            }
            else
            {
                method.Statements.Add(cmie);
            }

            ctd.Members.Add(method);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates a static method that delegates to the base
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddStaticDelegatingMethod(CodeTypeDeclaration ctd, Type typeDelegate, string name, Type returnType, Type[] paramTypes, string[] paramNames)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = name;
            method.Attributes = MemberAttributes.Public | MemberAttributes.Static;

            CodeMethodInvokeExpression cmie = new CodeMethodInvokeExpression();
            cmie.Method.TargetObject = new CodeTypeReferenceExpression(typeDelegate);
            cmie.Method.MethodName = name;

            if (paramTypes != null)
            {
                for (int i = 0; i < paramTypes.Length; i++)
                {
                    method.Parameters.Add(new CodeParameterDeclarationExpression(paramTypes[i], paramNames[i]));
                    cmie.Parameters.Add(new CodeArgumentReferenceExpression(paramNames[i]));
                }
            }

            if (returnType != null)
            {
                method.ReturnType = new CodeTypeReference(returnType);
                CodeMethodReturnStatement ret = new CodeMethodReturnStatement(cmie);
                method.Statements.Add(ret);
            }
            else
            {
                method.Statements.Add(cmie);
            }

            ctd.Members.Add(method);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates a read-only property that delegates to the base
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddDelegatingReadOnlyProperty(CodeTypeDeclaration ctd, string fieldName, string name, Type type)
        {
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = name;
            prop.Attributes = MemberAttributes.Public;
            prop.HasGet = true;
            prop.Type = new CodeTypeReference(type);

            CodeMethodReturnStatement ret = new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), name));
            prop.GetStatements.Add(ret);

            ctd.Members.Add(prop);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Create a static read-only property that delegates to the base
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddStaticDelegatingReadOnlyProperty(CodeTypeDeclaration ctd, Type typeDelegate, string name, Type type)
        {
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = name;
            prop.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            prop.HasGet = true;
            prop.Type = new CodeTypeReference(type);

            CodeMethodReturnStatement ret = new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeDelegate), name));
            prop.GetStatements.Add(ret);

            ctd.Members.Add(prop);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates in indexed property that delegates to the base indexed property
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddDelegatingIndexedProperty(CodeTypeDeclaration ctd, string fieldName, Type type, Type indexType, string indexName)
        {
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = "Item";
            prop.Attributes = MemberAttributes.Public;
            prop.HasGet = true;
            prop.HasSet = true;
            prop.Type = new CodeTypeReference(type);
            prop.Parameters.Add(new CodeParameterDeclarationExpression(indexType, indexName));

            CodeMethodReturnStatement ret = new CodeMethodReturnStatement(new CodeIndexerExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodeExpression[] { new CodeVariableReferenceExpression(indexName) }));
            prop.GetStatements.Add(ret);

            CodeAssignStatement asn = new CodeAssignStatement(new CodeIndexerExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodeExpression[] { new CodeVariableReferenceExpression(indexName) }), new CodePropertySetValueReferenceExpression());
            prop.SetStatements.Add(asn);

            ctd.Members.Add(prop);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates a strong type property accessor
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void CreateCodeForProperty(CodeTypeDeclaration type, ProfileNameTypeStruct property)
        {
            string name = property.Name;
            int pos = name.IndexOf('.');
            if (pos > 0)
                name = name.Substring(pos + 1);

            // e.g.: public string Color {
            //                       get { return (string) GetProperty("Color"); }
            //                       set { SetProperty("Color", value); } }


            // public  property.Type property.name {
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = name;
            prop.Attributes = MemberAttributes.Public;
            prop.HasGet = true;
            prop.Type = property.PropertyCodeRefType;

            ////////////////////////////////////////////////////////////
            // Get statements
            // get { return (property.type) GetProperty(property.name); }
            CodeMethodInvokeExpression cmie;
            CodeMethodReturnStatement getLine;

            cmie = new CodeMethodInvokeExpression();
            cmie.Method.TargetObject = new CodeThisReferenceExpression();
            cmie.Method.MethodName = "GetPropertyValue";
            cmie.Parameters.Add(new CodePrimitiveExpression(name));
            getLine = new CodeMethodReturnStatement(new CodeCastExpression(prop.Type, cmie));

            prop.GetStatements.Add(getLine);

            if (!property.IsReadOnly)
            {

                ////////////////////////////////////////////////////////////
                // Set statements
                // set { SetProperty(property.name, value); }
                CodeMethodInvokeExpression setLine;

                setLine = new CodeMethodInvokeExpression();
                setLine.Method.TargetObject = new CodeThisReferenceExpression();
                setLine.Method.MethodName = "SetPropertyValue";
                setLine.Parameters.Add(new CodePrimitiveExpression(name));
                setLine.Parameters.Add(new CodePropertySetValueReferenceExpression());
                prop.HasSet = true;
                prop.SetStatements.Add(setLine);
            }
            type.Members.Add(prop);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Creates a property accessor for a group and creates the group class
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddPropertyGroup(string groupName, string propertyNames, Hashtable properties, CodeTypeDeclaration type, CodeNamespace ns)
        {
            // e.g.: public string Foo {
            //                       get { return (ProfileGroupFooClass) GetProfileGroup("Foo"); } }

            // public  property.Type property.name {
            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = groupName;
            prop.Attributes = MemberAttributes.Public;
            prop.HasGet = true;
            prop.Type = new CodeTypeReference("WebProfileGroup" + groupName);

            ////////////////////////////////////////////////////////////
            // Get statements
            // get { return (property.type) GetProperty(property.name); }
            CodeMethodInvokeExpression cmie;
            CodeMethodReturnStatement getLine;

            cmie = new CodeMethodInvokeExpression();
            cmie.Method.TargetObject = new CodeThisReferenceExpression();
            cmie.Method.MethodName = "GetProfileGroup";
            cmie.Parameters.Add(new CodePrimitiveExpression(prop.Name));
            getLine = new CodeMethodReturnStatement(new CodeObjectCreateExpression(prop.Type, cmie));

            prop.GetStatements.Add(getLine);
            type.Members.Add(prop);

            // public class WebProfileGroupGroupName
            CodeTypeDeclaration grpType = new CodeTypeDeclaration();
            grpType.Name = "WebProfileGroup" + groupName;

            string[] grpProps = propertyNames.Split(';');
            foreach (string grpProp in grpProps)
            {
                // public string Color {
                //                       get { return (string) GetProperty("Color"); }
                //                       set { SetProperty("Color", value); } }
                CreateCodeForProperty(grpType, (ProfileNameTypeStruct)properties[grpProp]);
            }

            AddProfileGroupBaseDelegators(grpType);

            ns.Types.Add(grpType);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Generate Create function for creating a profile for a user
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private static void AddCodeForGetProfileForUser(CodeTypeDeclaration type)
        {
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = "GetProfile";
            method.Attributes = MemberAttributes.Public;
            method.ReturnType = new CodeTypeReference(type.Name);
            method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "username"));

            CodeMethodInvokeExpression cmie = new CodeMethodInvokeExpression();
            cmie.Method.TargetObject = new CodeTypeReferenceExpression(typeof(ProfileBase));
            cmie.Method.MethodName = "Create";
            cmie.Parameters.Add(new CodeArgumentReferenceExpression("username"));

            CodeMethodReturnStatement returnSatement = new CodeMethodReturnStatement(new CodeObjectCreateExpression(method.ReturnType, new CodeExpression[] { cmie }));

            method.Statements.Add(returnSatement);
            type.Members.Add(method);
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Holds modified data collected from config
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private class ProfileNameTypeStruct
        {
            internal string Name;
            internal CodeTypeReference PropertyCodeRefType;
            //internal Type PropertyType;
            internal bool IsReadOnly;
            //internal int LineNumber;
            //internal string FileName;
        }
        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Exception class
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private class WebProfileGeneratorException : Exception
        {
            public WebProfileGeneratorException(string message)
                : base(message)
            {
            }
        }

        ///-------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///      Access the docdata opened or closed
        /// </summary>
        ///-------------------------------------------------------------------------------------------------------------
        private class EditDocData : DocData
        {
            private RunningDocumentTable _rdt;
            private uint _cookie;

            ///-------------------------------------------------------------------------------------------------------------
            /// <summary>
            ///    Constructor.  Aquires edit lock on document.
            /// </summary>
            ///-------------------------------------------------------------------------------------------------------------
            public EditDocData(IServiceProvider serviceProvider, string fileName)
                : base(serviceProvider, fileName)
            {
                _rdt = new RunningDocumentTable(serviceProvider);

                // Locate and lock the document
                _rdt.FindDocument(fileName, out _cookie);
                _rdt.LockDocument(_VSRDTFLAGS.RDT_EditLock, _cookie);
            }

            ///-------------------------------------------------------------------------------------------------------------
            /// <summary>
            ///    Override of Dispose so we can free our lock after DocData.
            /// </summary>
            ///-------------------------------------------------------------------------------------------------------------
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (disposing)
                {
                    if (_cookie != 0 && _rdt != null)
                    {
                        // prevent recursion
                        uint cookie = _cookie;
                        _cookie = 0;

                        try
                        {
                            // Unlock the document, specifying to save if this is the last lock and the buffer is dirty
                            _rdt.UnlockDocument(_VSRDTFLAGS.RDT_EditLock | _VSRDTFLAGS.RDT_Unlock_SaveIfDirty, cookie);
                        }
                        finally
                        {
                            _cookie = 0;
                            _rdt = null;
                        }
                    }
                }
            }
        }
    }
}
