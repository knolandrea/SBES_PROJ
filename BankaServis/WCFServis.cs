using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace BankaServis
{
    public class WCFServis : IBanka
    {

        [PrincipalPermission(SecurityAction.Demand, Role = "Korisnici")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool Isplata(double suma)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool Opomena(string username)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool OtvoriRacun(string username)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Korisnici")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public double ProveriStanje()
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Korisnici")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public void Uplata(double suma)
        {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool ZatvoriRacun(string username)
        {
            throw new NotImplementedException();
        }
    }
}
