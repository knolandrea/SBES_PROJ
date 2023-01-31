using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	internal class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/WCFServis";
			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

			Console.WriteLine("Trenutni korisnik:  " + WindowsIdentity.GetCurrent().Name);

			//string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			string cltCertCN = "sluzbenik";
			string srvCertCN = "bankacert";

			/// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
			X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

			EndpointAddress endpointAddress = new EndpointAddress(new Uri(address), new X509CertificateEndpointIdentity(srvCert));



			using (WCFClient proxy = new WCFClient(binding, address))
			{
				proxy.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
				proxy.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ServerCertValidator();
				proxy.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
				proxy.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);


				try
				{
					proxy.CreateNewChannel();
					ClientMeni meni = new ClientMeni();
					meni.StartMeni(proxy);

				}
				catch (Exception e)
				{
					Console.WriteLine("[ERROR] {0}", e.Message);
					Console.WriteLine("[StackTrace] {0}", e.StackTrace);
					Console.WriteLine("[InnerException] {0}", e.InnerException.InnerException);
					Console.Read();
				}
				finally
				{
					Console.Read();
					proxy.Close();
				}

			}



		}
	}
}
