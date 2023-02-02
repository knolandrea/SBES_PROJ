using Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BankaServis
{
    internal class DBOperations
    {

        private static string xmlPath = "../../bazaRacuna.xml";

        public static void NapraviBazu()
        {
            if (!File.Exists(xmlPath))
            {
                XmlDocument baza = new XmlDocument();
                baza.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                using (XmlTextWriter xmlWriter = new XmlTextWriter(xmlPath, null))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    baza.Save(xmlWriter);
                }

                List<Racun> racuni = new List<Racun>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<Racun>));
                using (var writer = new StreamWriter(xmlPath))
                {
                    serializer.Serialize(writer, racuni);
                }
            }
        }
        public static bool NoviRacun(Racun racun)
        {
            List<Racun> racuni = UcitajRacune();

            if (racuni.Where(x => x.korisnickoIme == racun.korisnickoIme).FirstOrDefault() != null)
            {

                throw new FaultException($"{racun.korisnickoIme} vec ima otvoren racun.\n");

            }

            racuni.Add(racun);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Racun>));
            using (var writer = new StreamWriter(xmlPath))
            {
                serializer.Serialize(writer, racuni);
            }
            return true;
        }

        public static bool UkloniRacun(string username)
        {
            List<Racun> racuni = UcitajRacune();
            var racun = racuni.Where(x => x.korisnickoIme == username).FirstOrDefault();

            if (racun != null)
            {
                racuni.Remove(racun);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Racun>));
                using (var writer = new StreamWriter(xmlPath))
                {
                    serializer.Serialize(writer, racuni);
                }
                return true;

            }
            throw new FaultException($"Racun korisnika {racun.korisnickoIme} ne postoji.\n");
        }

        public static List<Racun> UcitajRacune()
        {
            List<Racun> racuni = new List<Racun>(1);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Racun>));
            using (var reader = new StreamReader(xmlPath))
            {
                racuni = (List<Racun>)serializer.Deserialize(reader);
            }

            return racuni;
        }

        public static double IzvuciStanje(string username)
        {
            List<Racun> racuni = UcitajRacune();
            var racun = racuni.Where(x => x.korisnickoIme == username).FirstOrDefault();

            if (racun != null)
            {
                return racun.stanje;
            }

            throw new FaultException($"Racun korisnika {racun.korisnickoIme} ne postoji.\n");
        }

        public static void Uplati(string username, double suma)
        {
            List<Racun> racuni = UcitajRacune();

            var racun = racuni.Where(x => x.korisnickoIme == username).FirstOrDefault();
            if (racun != null)
            {
                var index = racuni.IndexOf(racun);
                racuni[index].stanje += suma;

                if (racuni[index].stanje >= 0)
                {
                    racuni[index].blokiran = 0; // 0-> nije blokiran
                }


                XmlSerializer serializer = new XmlSerializer(typeof(List<Racun>));
                using (var writer = new StreamWriter(xmlPath))
                {
                    serializer.Serialize(writer, racuni);
                }

                return;
            }

            throw new FaultException($"Racun korisnika {racun.korisnickoIme} ne postoji.\n");
        }


        public static bool Isplati(string username, double suma)
        {
            List<Racun> racuni = UcitajRacune();
            var racun = racuni.Where(x => x.korisnickoIme == username).FirstOrDefault();
            if (racun != null)
            {
                if (racun.blokiran == 1)
                {
                    throw new FaultException("Racun vam je blokiran! Ne mozete podici novac.\n");
                }

                if (racun.blokiran == 0)
                {
                    if (racun.stanje - suma < -1 * racun.dozvoljenMinus)
                    {
                        throw new FaultException($"Nije moguce podici zeljenu svotu jer prevazilazi dozvoljeni minus.\n");
                    }
                }

                var index = racuni.IndexOf(racun);
                racuni[index].stanje -= suma;

                XmlSerializer serializer = new XmlSerializer(typeof(List<Racun>));
                using (var writer = new StreamWriter(xmlPath))
                {
                    serializer.Serialize(writer, racuni);
                }
                return true;
            }

            throw new FaultException($"Racun korisnika {racun.korisnickoIme} ne postoji.\n");
        }

        public static bool Blojiraj(string username)
        {

            List<Racun> racuni = UcitajRacune();
            var racun = racuni.Where(x => x.korisnickoIme == username).FirstOrDefault();

            if (racun != null)
            {
                var index = racuni.IndexOf(racun);

                if (racun.stanje >= 0)
                {
                    throw new FaultException($"Korisnik nije u minusu.\n");
                }
                racuni[index].blokiran = 1; // 1-> blokiran
                XmlSerializer serializer = new XmlSerializer(typeof(List<Racun>));
                using (var writer = new StreamWriter(xmlPath))
                {
                    serializer.Serialize(writer, racuni);
                }
                return true;

            }

            throw new FaultException($"Racun korisnika {racun.korisnickoIme} ne postoji.\n");
        }

    }
}
