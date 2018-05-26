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
                do osobnik.losujChromosom();
                while (czyIstnieje(osobnik, Populacja));
                Populacja.Add(osobnik);
            }
            Populacja.Sort(porownaj);
            //ustalPrawdopodobienstwa(Populacja);
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
            drukuj();
            List<Osobnik> nowaPopulacja = new List<Osobnik>(), turniej = new List<Osobnik>();
            Osobnik nowyOsobnik;
            
            int q = 3, n;
            while (Populacja.Count > 0)
            {
                for (int i = 0; i < q; i++)
                {
                    if (Populacja.Count == 0) break;
                    int k = rand.Next(Populacja.Count);
                    Osobnik osobnik = Populacja.ElementAt(k);
                    turniej.Add(osobnik);
                    Populacja.RemoveAt(k);
                }
                turniej.Sort(porownaj);
                nowaPopulacja.Add(turniej.ElementAt(0));
                turniej.Clear();
            } 
            n = nowaPopulacja.Count; 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (i != j)// && los(PrawdopodobienstwoKrzyzowania))
                    {
                        nowyOsobnik = krzyzowanie(nowaPopulacja.ElementAt(i), nowaPopulacja.ElementAt(j));
                        nowaPopulacja.Add(nowyOsobnik);
                    }
                //if (los(PrawdopodobienstwoMutacji))
                {
                    nowyOsobnik = mutacja(nowaPopulacja.ElementAt(i));
                    nowaPopulacja.Add(nowyOsobnik);
                }
            }
            foreach (Osobnik osobnik in nowaPopulacja)
                osobnik.wyliczPrzystosowanie();
            nowaPopulacja.Sort(porownaj);
            //ustalPrawdopodobienstwa(nowaPopulacja);
            Populacja = nowaPopulacja;
            //if (Populacja.Count > RozmiarPopulacji)
                //Populacja.RemoveRange(RozmiarPopulacji, Populacja.Count - RozmiarPopulacji);

            /*
            ustalPrawdopodobienstwa(noweOsobniki);
            Populacja.Clear();
            foreach (Osobnik osobnik in noweOsobniki)
            {
                if (los(osobnik.Prawdopodobienstwo))
                    Populacja.Add(osobnik);
            }
            */
        }

        /* zwraca najlepszego osobnika z aktualnej populacji */
        public Osobnik najlepszyOsobnik()
        {
            return Populacja.ElementAt(0);
        }

        /* wyświetla aktualną populację */
        public void drukuj()
        {
            //foreach (Osobnik osobnik in Populacja)
                //Console.WriteLine(osobnik.ToString());
           // Console.WriteLine();
            Console.WriteLine(Populacja.ElementAt(0).ToString());
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
                if (los(50))
                    nowyOsobnik.Chromosom[i] = DaneWejsciowe.Instancja.Automaty[rand.Next(DaneWejsciowe.Instancja.LiczbaAutomatow)];
                else
                    nowyOsobnik.Chromosom[i] = osobnik.Chromosom[i];
            }
            return nowyOsobnik;
        }
        
        /* do selekcji - lista w parametrze MUSI być już posortowana względem przystosowania */
        private void ustalPrawdopodobienstwa(List<Osobnik> populacja)
        {
            double P = 0.7, suma = 0, p;
            p = P;
            foreach (Osobnik osobnik in populacja)
            {
                osobnik.Prawdopodobienstwo = (int)(100 * p);
                suma += p;
                p = (1 - suma) * P;
                if (p <= 0) return;
            }
        }

        /* sprawdza czy dany osobnik istnieje już w populacji */
        private bool czyIstnieje(Osobnik osobnik, List<Osobnik> populacja)
        {
            int n = DaneWejsciowe.Instancja.LiczbaRund;
            foreach (Osobnik osobnik2 in Populacja)
            {
                int i;
                for (i = 0; i < n; i++)
                    if (osobnik.Chromosom[i].ID != osobnik2.Chromosom[i].ID)
                        break;
                if (i == n) return true;
            }
            return false;
        }

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
