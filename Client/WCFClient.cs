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

namespace Client
{
    public class WCFClient : ChannelFactory<IBanka>, IBanka, IDisposable
    {
        IBanka factory;

        public WCFClient(NetTcpBinding binding, string address) : base(binding, address)
        {
        }

        public WCFClient(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
        }

        public void CreateNewChannel()
        {
            factory = this.CreateChannel();
        }

        public bool Isplata(double suma)
        {
            try
            {
                return factory.Isplata(suma);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool Opomena(string username)
        {
            try
            {
                return factory.Opomena(username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool OtvoriRacun(string username)
        {
            bool retVal = false;

            try
            {
                retVal = factory.OtvoriRacun(username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return retVal;
         
        }

        public double ProveriStanje()
        {
            try
            {
                return factory.ProveriStanje();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public void Uplata(double suma)
        {
            try
            {
                factory.Uplata(suma);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool ZatvoriRacun(string username)
        {
            try
            {
                return factory.ZatvoriRacun(username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
