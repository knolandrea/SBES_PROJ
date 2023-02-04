using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Manager;

namespace BankaServis
{
    public class WCFServis : IBanka
    {
        //korisnici rade iskljucivo sa svojim racunom, a sluzbenici sa racunima drugih korisnika

        [PrincipalPermission(SecurityAction.Demand, Role = "Korisnici")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool Isplata(double suma)
        {
                var identity = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                return DBOperations.Isplati(Formatter.GetName(identity), suma);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool Opomena(string username)
        {
            return DBOperations.Blokiraj(username);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool OtvoriRacun(string username)
        {
            return DBOperations.NoviRacun(new Racun(username));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Korisnici")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public double ProveriStanje()
        {
                var identity = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                return DBOperations.IzvuciStanje(Formatter.GetName(identity));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Korisnici")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public void Uplata(double suma)
        {
                var identity = ServiceSecurityContext.Current.PrimaryIdentity.Name;
                DBOperations.Uplati(Formatter.GetName(identity), suma);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sluzbenici")]
        public bool ZatvoriRacun(string username)
        {
            return DBOperations.UkloniRacun(username);
        }
    }
}
