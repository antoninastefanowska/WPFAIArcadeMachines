using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class Osobnik : IComparable<Osobnik>
    {
        public Automat[] Chromosom { get; set; } /* chromosom jest sekwencją automatów */
        public int Przystosowanie { get; set; } /* przystosowanie jest równe sumie wygranej na automatach zgodnie z sekwencją w chromosomie */

        private Random rand;

        public Osobnik()
        {
            rand = new Random();
            Chromosom = new Automat[DaneWejsciowe.Instancja.LiczbaRund];
            Przystosowanie = 0;
        }

        /* funkcja do porównań względem przystosowania */
        public int CompareTo(Osobnik other)
        {
            if (this.Przystosowanie == other.Przystosowanie)
                return 0;
            else if (this.Przystosowanie > other.Przystosowanie)
                return 1;
            else return -1;
        }

        /* generuje losowy chromosom */
        public void LosujChromosom()
        {
            int n = DaneWejsciowe.Instancja.LiczbaRund, k = DaneWejsciowe.Instancja.LiczbaAutomatow;
            for (int i = 0; i < n; i++)
                Chromosom[i] = DaneWejsciowe.Instancja.Automaty[rand.Next(k)];
            WyliczPrzystosowanie();
        }

        /* gra po kolei na każdym automacie według sekwencji */
        public void WyliczPrzystosowanie()
        {
            int suma = 0, n = DaneWejsciowe.Instancja.LiczbaRund;
            for (int i = 0; i < n; i++)
                suma += Chromosom[i].Zagraj();
            DaneWejsciowe.Instancja.ResetujAutomaty();
            Przystosowanie = suma;
        }

        public override string ToString()
        {
            int n = DaneWejsciowe.Instancja.LiczbaRund;
            string s = "";
            for (int i = 0; i < n; i++)
                s += Chromosom[i].ID.ToString() + ' ';
            s += "- " + Przystosowanie.ToString();
            return s;
        }
    }
}
