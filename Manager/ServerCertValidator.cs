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
	public class ServerCertValidator : X509CertificateValidator
	{
		public override void Validate(X509Certificate2 certificate)
		{
            var clnCrt = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            X509Certificate2 clientCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clnCrt);

            if (certificate.Issuer != clientCert.Issuer)
            {
                throw new Exception("Certificate is not valid!");
            }
        }
	}
}
