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
			//X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));
			X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "bankacert");

			if (certificate.Subject.Equals(certificate.Issuer) && (!certificate.Issuer.Equals(srvCert.Issuer)))
			{
				throw new Exception("Certificate is not valid.");
			}

		}
	}
}
