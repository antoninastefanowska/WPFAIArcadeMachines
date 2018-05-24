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
        public Automat[] Chromosom { get; set; }
        public int Prawdopodobienstwo { get; set; }
        public int Przystosowanie { get; set; }

        private Random rand;

        public Osobnik()
        {
            rand = new Random();
            LiczbaRund = DaneWejsciowe.Instancja.LiczbaRund;
            Chromosom = new Automat[LiczbaRund];
            Przystosowanie = 0;
            Prawdopodobienstwo = 0;
        }

        public void losujChromosom()
        {
            for (int i = 0; i < LiczbaRund; i++)
                Chromosom[i] = DaneWejsciowe.Instancja.Automaty[rand.Next(7)];
            wyliczPrzystosowanie();
        }

        public void wyliczPrzystosowanie()
        {
            int suma = 0;
            for (int i = 0; i < LiczbaRund; i++)
                suma += Gen[i].zagraj();
            Przystosowanie = suma;
        }
    }
}
