using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class AlgorytmGenetyczny
    {
        public List<Osobnik> Populacja { get; set; }
        public int RozmiarPopulacjiPoczatkowej { get; set; }
        public int MaksymalnyRozmiarPopulacji { get; set; }
        public int RozmiarTurnieju { get; set; }
        public int PrawdopodobienstwoKrzyzowania { get; set; } /* w procentach */
        public int PrawdopodobienstwoMutacji { get; set; } /* w procentach */

        private Random rand;

        public AlgorytmGenetyczny(int rozmiarPopulacjiPoczatkowej, int maksymalnyRozmiarPopulacji, int rozmiarTurnieju, int prawdopodobienstwoKrzyzowania, int prawdopodobienstwoMutacji)
        {
            rand = new Random();
            RozmiarPopulacjiPoczatkowej = rozmiarPopulacjiPoczatkowej;
            RozmiarTurnieju = rozmiarTurnieju;
            MaksymalnyRozmiarPopulacji = maksymalnyRozmiarPopulacji;
            Populacja = new List<Osobnik>();
            for (int i = 0; i < RozmiarPopulacjiPoczatkowej; i++)
            {
                Osobnik osobnik = new Osobnik();
                do osobnik.LosujChromosom(); /* losuje populację początkową */
                while (CzyIstnieje(osobnik, Populacja));
                Populacja.Add(osobnik);
            }
            PrawdopodobienstwoKrzyzowania = prawdopodobienstwoKrzyzowania;
            PrawdopodobienstwoMutacji = prawdopodobienstwoMutacji;
        }

        /* jeden przebieg iteracji algorytmu genetycznego */
        public void Iteracja()
        {
            List<Osobnik> nowaPopulacja = new List<Osobnik>(), populacjaRodzicielska;
            Osobnik nowyOsobnik;

            populacjaRodzicielska = Turniej(RozmiarTurnieju, Populacja); // wyselekcjonowanie populacji rodzicielskiej metodą turniejową
            for (int i = 0; i < populacjaRodzicielska.Count; i++)
            {
                if (Los(PrawdopodobienstwoKrzyzowania))
                {
                    for (int j = 0; j < populacjaRodzicielska.Count; j++)
                    {
                        if (i != j)
                        {
                            nowyOsobnik = Krzyzowanie(populacjaRodzicielska.ElementAt(i), populacjaRodzicielska.ElementAt(j));
                            nowaPopulacja.Add(nowyOsobnik);
                        }
                        if (nowaPopulacja.Count >= MaksymalnyRozmiarPopulacji - 1) break;
                    }
                }
                if (nowaPopulacja.Count >= MaksymalnyRozmiarPopulacji - 1) break;
                if (Los(PrawdopodobienstwoMutacji))
                {
                    nowyOsobnik = Mutacja(populacjaRodzicielska.ElementAt(i));
                    nowaPopulacja.Add(nowyOsobnik);
                }
            }
            nowaPopulacja.Add(populacjaRodzicielska.Max()); // przeniesienie najlepszego osobnika z populacji rodzicielskiej do nowej populacji
            foreach (Osobnik osobnik in nowaPopulacja)
                osobnik.WyliczPrzystosowanie();
            Populacja = nowaPopulacja;
        }

        /* selekcja turniejowa */
        private List<Osobnik> Turniej(int rozmiarTurnieju, List<Osobnik> populacja)
        {
            List<Osobnik> turniej = new List<Osobnik>(), populacjaRodzicielska = new List<Osobnik>();
            if (populacja.Count > rozmiarTurnieju)
            {
                while (populacja.Count > 0 && populacjaRodzicielska.Count < MaksymalnyRozmiarPopulacji)
                {
                    for (int i = 0; i < rozmiarTurnieju; i++)
                    {
                        if (populacja.Count == 0) break;
                        int k = rand.Next(Populacja.Count);
                        Osobnik osobnik = Populacja.ElementAt(k);
                        turniej.Add(osobnik);
                        populacja.RemoveAt(k);
                    }
                    populacjaRodzicielska.Add(turniej.Max());
                    turniej.Clear();
                }
                return populacjaRodzicielska;
            }
            else return populacja;
        }

        /* zwraca najlepszego osobnika z aktualnej populacji */
        public Osobnik NajlepszyOsobnik()
        {
            return Populacja.Max();
        }

        /* krzyżowanie jednostajne */
        private Osobnik Krzyzowanie(Osobnik osobnik1, Osobnik osobnik2)
        {
            Osobnik nowyOsobnik = new Osobnik();
            int n = DaneWejsciowe.Instancja.LiczbaRund;
            for (int i = 0; i < n; i++)
            {
                if (Los(50)) // wymiana genu nastąpi z prawdopodobieństwem 50%
                    nowyOsobnik.Chromosom[i] = osobnik1.Chromosom[i];
                else
                    nowyOsobnik.Chromosom[i] = osobnik2.Chromosom[i];
            }
            return nowyOsobnik;
        }

        private Osobnik Mutacja(Osobnik osobnik)
        {
            Osobnik nowyOsobnik = new Osobnik();
            int n = DaneWejsciowe.Instancja.LiczbaRund, k = DaneWejsciowe.Instancja.LiczbaAutomatow;
            for (int i = 0; i < n; i++)
            {
                if (Los(50)) // zmutowanie danego genu nastąpi z prawdopodobieństwem 50%
                    nowyOsobnik.Chromosom[i] = DaneWejsciowe.Instancja.Automaty[rand.Next(k)]; // gen zostaje podmieniony na wylosowany
                else
                    nowyOsobnik.Chromosom[i] = osobnik.Chromosom[i];
            }
            return nowyOsobnik;
        }

        /* sprawdza czy dany osobnik istnieje już w populacji */
        private bool CzyIstnieje(Osobnik osobnik, List<Osobnik> populacja)
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
        private bool Los(int prawdopodobienstwo)
        {
            int los = rand.Next(100);
            if (los < prawdopodobienstwo)
                return true;
            else return false;
        }
    }
}
