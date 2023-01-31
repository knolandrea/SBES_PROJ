using Contracts;
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

namespace BankaServis
{
    public class Program
    {
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
			string address = "net.tcp://localhost:9999/WCFServis";
			ServiceHost host = new ServiceHost(typeof(WCFServis));

			//string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			string srvCertCN = "bankacert";

			//authentification
			host.AddServiceEndpoint(typeof(IBanka), binding, address);
			host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
			host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
			host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
			host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;


			try
			{
				host.Open();
				Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");
				Console.ReadLine();
			}
			catch (Exception e)
			{
				Console.WriteLine("[ERROR] {0}", e.Message);
				Console.WriteLine("[StackTrace] {0}", e.StackTrace);
			}
			finally
			{
				Console.Read();
				host.Close();
			}
		}
	}
}
