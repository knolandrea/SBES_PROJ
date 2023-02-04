using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
	public class ClientCertValidator : X509CertificateValidator
	{
		public override void Validate(X509Certificate2 certificate)
		{
			// *** user -> banka, CN= bankacert
			//string ime = Formatter.ParseName(WindowsIdentity.GetCurrent().Name) + "cert";
			string ime = "bankacert";
			X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, ime);

			//klijentov sertifikat je validan ako mu je issuer isti kao servisov
			if (certificate.Issuer != srvCert.Issuer)
			{
				Audit.AuthenticationFailure(certificate.SubjectName.Name);
				throw new Exception("Client cert is not valid. Different issuers\n");
			}
			Audit.AuthenticationSuccess(certificate.SubjectName.Name);
		}
	}
}
