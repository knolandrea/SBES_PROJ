using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IBanka
    {
        [OperationContract]
        bool OtvoriRacun(string username);
        [OperationContract]
        bool ZatvoriRacun(string username);
        [OperationContract]
        double ProveriStanje();
        [OperationContract]
        void Uplata(double suma);
        [OperationContract]
        bool Isplata(double suma);
        [OperationContract]
        bool Opomena(string username);
    }
}
