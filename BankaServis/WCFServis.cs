using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace BankaServis
{
    public class WCFServis : IBanka
    {
        public bool Isplata(double suma)
        {
            throw new NotImplementedException();
        }

        public bool Opomena(string username)
        {
            throw new NotImplementedException();
        }

        public bool OtvoriRacun(string username)
        {
            throw new NotImplementedException();
        }

        public double ProveriStanje()
        {
            throw new NotImplementedException();
        }

        public void Uplata(double suma)
        {
            throw new NotImplementedException();
        }

        public bool ZatvoriRacun(string username)
        {
            throw new NotImplementedException();
        }
    }
}
