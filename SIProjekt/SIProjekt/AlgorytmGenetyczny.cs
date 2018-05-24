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
            Populacja.Sort(porownaj);
            PrawdopodobienstwoKrzyzowania = prawdopodobienstwoKrzyzowania;
            PrawdopodobienstwoMutacji = prawdopodobienstwoMutacji;
        }

        /* funkcja do sortowania listy osobników względem przystosowania */
        public static int porownaj(Osobnik osobnik1, Osobnik osobnik2)
        {
            if (osobnik1.Przystosowanie == osobnik2.Przystosowanie)
                return 0;
            else if (osobnik1.Przystosowanie > osobnik2.Przystosowanie)
                return -1;
            else return 1;
        }

        /* jeden przebieg iteracji algorytmu genetycznego */
        public void iteracja()
        {
            List<Osobnik> nowaPopulacja = new List<Osobnik>();          
            //ustalPrawdopodobienstwa();
            drukuj();
            for (int i = 0; i < RozmiarPopulacji; i++)
            {
                for (int j = 0; j < RozmiarPopulacji; j++)
                    if (i != j && los(PrawdopodobienstwoKrzyzowania))
                        Populacja.Add(krzyzowanie(Populacja.ElementAt(i), Populacja.ElementAt(j)));
                if (los(PrawdopodobienstwoMutacji))
                    Populacja.Add(mutacja(Populacja.ElementAt(i)));
            }
            foreach (Osobnik osobnik in Populacja)
                osobnik.wyliczPrzystosowanie();
            Populacja.Sort(porownaj);
            if (Populacja.Count > RozmiarPopulacji)
                Populacja.RemoveRange(RozmiarPopulacji, Populacja.Count - RozmiarPopulacji);
        }

        /* wyświetla aktualną populację */
        public void drukuj()
        {
            foreach (Osobnik osobnik in Populacja)
                Console.WriteLine(osobnik.ToString());
            Console.WriteLine();
            //Console.WriteLine(Populacja.ElementAt(0).ToString());
        }

        private Osobnik krzyzowanie(Osobnik osobnik1, Osobnik osobnik2)
        {
            Osobnik nowyOsobnik = new Osobnik();
            int n = DaneWejsciowe.Instancja.LiczbaRund;
            for (int i = 0; i < n; i++)
            {
                if (los(50))
                    nowyOsobnik.Chromosom[i] = osobnik1.Chromosom[i];
                else
                    nowyOsobnik.Chromosom[i] = osobnik2.Chromosom[i];
            }
            return nowyOsobnik;
        }

        private Osobnik mutacja(Osobnik osobnik)
        {
            Osobnik nowyOsobnik = new Osobnik();
            int n = DaneWejsciowe.Instancja.LiczbaRund;
            for (int i = 0; i < n; i++)
            {
                if (los(30))
                    nowyOsobnik.Chromosom[i] = DaneWejsciowe.Instancja.Automaty[rand.Next(DaneWejsciowe.Instancja.LiczbaAutomatow)];
                else
                    nowyOsobnik.Chromosom[i] = osobnik.Chromosom[i];
            }
            return nowyOsobnik;
        }

        /*
        private void ustalPrawdopodobienstwa()
        {
            double p = 0.3, suma = 0;
            foreach (Osobnik osobnik in Populacja)
            {
                osobnik.Prawdopodobienstwo = (int)(100 * p);
                suma += p;
                p = (1 - suma) * p;
                if (p <= 0) return;
            }
        } */

        /* true wystapi z prawdopodobienstwem podanym w parametrze (w procentach) */
        private bool los(int prawdopodobienstwo)
        {
            int los = rand.Next(100);
            if (los < prawdopodobienstwo)
                return true;
            else return false;
        }
    }
}
