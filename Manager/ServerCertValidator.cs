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
           // var clnCrt = "sluzbenik";

            X509Certificate2 clientCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clnCrt);

            //serverov cert je validan ako je issuer isti kao klijentov i ako nije self signed
            if (certificate.Issuer != clientCert.Issuer)
            {
                Audit.AuthenticationFailure(certificate.SubjectName.Name);
                throw new Exception("Server cert is not valid! Different issuers.\n");
            }

           if (certificate.Issuer == certificate.Subject)
           {
                Audit.AuthenticationFailure(certificate.SubjectName.Name);
                throw new Exception("Server cert is not valid. It's self signed. \n");
           }

            Audit.AuthenticationSuccess(certificate.SubjectName.Name);
        }
	}
}
