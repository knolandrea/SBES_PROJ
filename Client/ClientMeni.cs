using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class ClientMeni
    {
        public void StartMeni(WCFClient proxy)
        {
            Console.WriteLine("----DOBRODOSLI U BANKU-----");
            string opcija = "";

            while(opcija != "x")
            {
                IzlistajOpcije();
                opcija = Console.ReadLine();
                Obrada(opcija, proxy);
            }
        }

        public void IzlistajOpcije()
        {
            Console.WriteLine("Odaberite jednu od sledecih opcija: ");
            Console.WriteLine("1 - Otvori racun (sluzbenici)");
            Console.WriteLine("2 - Zatvori racun (sluzbenici)");
            Console.WriteLine("3 - Opomena (sluzbenici)");
            Console.WriteLine("4 - Uplata");
            Console.WriteLine("5 - Isplata");
            Console.WriteLine("6 - Provera stanja");
            Console.WriteLine("x - exit");
        }

        public void Obrada(string opcija, WCFClient proxy)
        {
            string ime = "";
            double svota = 0;

            switch(opcija)
            {
                case "1":
                    Console.WriteLine("Unesite ime korisnika kome otvarate racun: ");
                    ime = Console.ReadLine();
                    if(proxy.OtvoriRacun(ime))
                    {
                        Console.WriteLine($"Uspesno otvoren racun za {ime}");
                    }
                    break;


                case "2":
                    Console.WriteLine("Unesite ime korisnika kome zatvarate racun: ");
                    ime = Console.ReadLine();
                    if (proxy.ZatvoriRacun(ime))
                    {
                        Console.WriteLine($"Uspesno zatvoren racun za {ime}");
                    }
                    break;


                case "3":
                    Console.WriteLine("Unesite ime korisnika kome blokirate racun: ");
                    ime = Console.ReadLine();
                    if (proxy.Opomena(ime))
                    {
                        Console.WriteLine($"Uspesno blokiran racun za {ime}");
                    }
                    break;


                case "4":
                    Console.WriteLine("Unesite svotu za uplatu: ");
                    ime = Console.ReadLine();
                    if(Double.TryParse(ime, out svota))
                    {
                        proxy.Uplata(svota);
                        Console.WriteLine("Uspesna uplata.");
                    }
                    else
                    {
                        Console.WriteLine("Broj koji ste uneli nije mogao biti ucitan");
                    }
                    break;


                case "5":
                    Console.WriteLine("Unesite svotu za isplatu: ");
                    ime = Console.ReadLine();
                    if (Double.TryParse(ime, out svota))
                    {
                        if(proxy.Isplata(svota))
                        {
                            Console.WriteLine("Uspesno isplacen novac.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Broj koji ste uneli nije mogao biti ucitan");
                    }
                    break;


                case "6":
                    svota = proxy.ProveriStanje();
                    Console.WriteLine($"Trenutno stanje na racunu: {svota}");
                    break;

                case "x":
                    Console.WriteLine("Dovidjenja");
                    return;

                default:
                    Console.WriteLine("Odaberite validnu opciju.");
                    break;
            }
        }
    }
}
