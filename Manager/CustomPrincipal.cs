using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    internal class CustomPrincipal : IPrincipal
    {
        IIdentity identity = null;
        public CustomPrincipal(IIdentity certificateIdentity)
        {
            identity = certificateIdentity;
        }

        public IIdentity Identity
        {
            get { return identity; }
        }


        public bool IsInRole(string role)
        {
            Type x509IdentityType = identity.GetType();

            // The certificate is stored inside a private field of this class
            FieldInfo certificateField = x509IdentityType.GetField("certificate", BindingFlags.Instance | BindingFlags.NonPublic);

            X509Certificate2 certificate = (X509Certificate2)certificateField.GetValue(identity);

            var subject = certificate.SubjectName.Name;
            var ou = GetOU(subject);

            if (role.Equals(ou))
            {
                return true;
            }

            return false;
        }



        //"CN=xy, OU=abc" => vraca string pocevsi od 4.karaktera u "OU=abc"
        static string GetOU(string subject)
        {
            string[] fields = subject.Split(',');
            foreach (string field in fields)
            {
                if (field.StartsWith("OU="))
                {
                    return field.Substring(3);
                }
            }
            return null;
        }
    }
}
