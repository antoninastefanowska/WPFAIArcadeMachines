using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class Osobnik
    {
        public int LiczbaRund { get; set; }
        public Automat[] Chromosom { get; set; } /* chromosom jest sekwencją automatów, na których się gra */
        public int Prawdopodobienstwo { get; set; } /* prawdopodobieństwo włączenia osobnika do populacji (chyba niepotrzebne?) */
        public int Przystosowanie { get; set; } /* przystosowanie jest równe sumie wygranej na automatach zgodnie z sekwencją w chromosomie */

        private Random rand;

        public Osobnik()
        {
            rand = new Random();
            LiczbaRund = DaneWejsciowe.Instancja.LiczbaRund;
            Chromosom = new Automat[LiczbaRund];
            Przystosowanie = 0;
            Prawdopodobienstwo = 0;
        }

        /* generuje losowy chromosom */
        public void losujChromosom()
        {
            for (int i = 0; i < LiczbaRund; i++)
                Chromosom[i] = DaneWejsciowe.Instancja.Automaty[rand.Next(DaneWejsciowe.Instancja.LiczbaAutomatow)];
            wyliczPrzystosowanie();
        }
        
        public void wyliczPrzystosowanie()
        {
            int suma = 0;
            for (int i = 0; i < LiczbaRund; i++)
            {
                if (Chromosom[i] == null) Console.WriteLine("null: " + i.ToString());
                suma += Chromosom[i].zagraj();
            }
            Przystosowanie = suma;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < LiczbaRund; i++)
                s += Chromosom[i].ID.ToString() + ' ';
            s += "- " + Przystosowanie.ToString() + ' ' + Prawdopodobienstwo.ToString();
            return s;
        }
    }
}
