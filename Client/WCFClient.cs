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
            Console.WriteLine("ovo je samo test\n");
            return 0;
            // throw new NotImplementedException();
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
