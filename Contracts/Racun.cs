using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [DataContract]
    public class Racun
    {
        [DataMember]
        public long brojRacuna { get; set; }
        [DataMember]
        public double stanje { get; set; }
        [DataMember]
        public double dozvoljenMinus { get; set; }
        [DataMember]
        public double blokiran { get; set; }
        [DataMember]
        public DateTime poslednjaTransakcija { get; set; }
        [DataMember]
        public string korisnickoIme { get; set; }


        private long BrRacunaGen()
        {
            long number = (long)(1000000000000000L + new Random().NextDouble() * 9000000000000000L); //15digit rand number
            return number;
        }
        
        public Racun() { }
        public Racun(string username)
        {
            this.brojRacuna = BrRacunaGen();
            this.stanje = 0;
            this.dozvoljenMinus = 1000;
            this.blokiran = 0;
            this.poslednjaTransakcija = DateTime.MinValue;
            this.korisnickoIme = username;
        }
    }
}
