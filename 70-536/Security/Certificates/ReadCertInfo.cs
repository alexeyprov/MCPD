using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

internal static class ReadCertInfo
{
	private static void Main()
	{
		X509Certificate2 cert = GetCertificateFromStoreCore(
			StoreName.My,
			StoreLocation.CurrentUser,
			X509FindType.FindBySubjectName,
			"SignedByCA");
		
		bool canDoKeyExchange = CanDoKeyExchange(cert);

		Console.WriteLine(canDoKeyExchange);
	}

	private static bool CanDoKeyExchange(X509Certificate2 cert)
	{
		RSACryptoServiceProvider rsaCsp = cert.PrivateKey as RSACryptoServiceProvider;
		CspKeyContainerInfo keyContainerInfo = rsaCsp != null ? rsaCsp.CspKeyContainerInfo : null;
		return keyContainerInfo != null && keyContainerInfo.KeyNumber == KeyNumber.Exchange;
	}

	private static X509Certificate2 GetCertificateFromStoreCore(StoreName storeName, StoreLocation storeLocation, X509FindType findType, object findValue)
	{
		X509CertificateStore x509CertificateStore = new X509CertificateStore(storeName, storeLocation);
		X509Certificate2Collection x509Certificate2Collection = null;
		X509Certificate2 result;
		try
		{
			x509CertificateStore.Open(OpenFlags.ReadOnly);
			x509Certificate2Collection = x509CertificateStore.Find(findType, findValue, false);
			if (x509Certificate2Collection.Count == 1)
			{
				result = new X509Certificate2(x509Certificate2Collection[0]);
			}
			else
			{
				throw new ArgumentException("More than one found!");
			}
		}
		finally
		{
			ResetAllCertificates(x509Certificate2Collection);
			x509CertificateStore.Close();
		}
		return result;
	}

	private static void ResetAllCertificates(X509Certificate2Collection certificates)
	{
		if (certificates != null)
		{
			for (int i = 0; i < certificates.Count; i++)
			{
				certificates[i].Reset();
			}
		}
	}
}
