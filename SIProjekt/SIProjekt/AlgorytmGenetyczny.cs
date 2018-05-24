using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class AlgorytmGenetyczny
    {
        public int RozmiarPopulacji { get; set; }
        public List<Osobnik> Populacja { get; set; }
        public int PrawdopodobienstwoKrzyzowania { get; set; }
        public int PrawdopodobienstwoMutacji { get; set; }

        private Random rand;

        public AlgorytmGenetyczny(int rozmiarPopulacji, int prawdopodobienstwoKrzyzowania, int prawdopodobienstwoMutacji)
        {
            rand = new Random();
            RozmiarPopulacji = rozmiarPopulacji;
            Populacja = new List<Osobnik>(RozmiarPopulacji);
            for (int i = 0; i < RozmiarPopulacji; i++)
            {
                Osobnik osobnik = new Osobnik();
                osobnik.losujChromosom();
                Populacja.Add(osobnik);
            }
            PrawdopodobienstwoKrzyzowania = prawdopodobienstwoKrzyzowania;
            PrawdopodobienstwoMutacji = prawdopodobienstwoMutacji;
        }

        public void iteracja()
        {
            List<Osobnik> nowaPopulacja = new List<Osobnik>();
            foreach(Osobnik osobnik1 in Populacja)
            {
                foreach(Osobnik osobnik2 in Populacja)
                    
            }
        }

        private void krzyzowanie(Osobnik osobnik1, Osobnik osobnik2)
        {
            int n = DaneWejsciowe.Instancja.LiczbaRund;
            Automat temp;
            for (int i = 0; i < n; i++)
                if (czyWylosowany(PrawdopodobienstwoKrzyzowania))
                {
                    temp = osobnik1.Chromosom[i];
                    osobnik1.Chromosom[i] = osobnik2.Chromosom[i];
                    osobnik2.Chromosom[i] = temp;
                }
        }

        private void mutacja(Osobnik osobnik)
        {
            int n = DaneWejsciowe.Instancja.LiczbaRund;
            for (int i = 0; i < n; i++)
                if (czyWylosowany(PrawdopodobienstwoMutacji))
                    osobnik.Chromosom[i] = DaneWejsciowe.Instancja.Automaty[rand.Next(7)];
        }

        private void ustalPrawdopodobienstwa()
        {
            double p = 0.3, suma = 0;
            Populacja.Sort(); //określić komparator na bazie przystosowania
            foreach (Osobnik osobnik in Populacja)
            {
                osobnik.Prawdopodobienstwo = (int)(100 * p);
                suma += p;
                p = (1 - suma) * p;
                if (p <= 0) return;
            }
        }

        private bool czyWylosowany(int prawdopodobienstwo)
        {
            int los = rand.Next(100);
            if (los < prawdopodobienstwo)
                return true;
            else return false;
        }
    }
}
