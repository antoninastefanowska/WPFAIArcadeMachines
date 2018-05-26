using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class Automat
    {
        public int ID { get; set; }
        public List<Nagroda> Nagrody { get; set; }
        public int OstatniaWygrana { get; set; }
        public int NumerNagrody { get; set; }

        private Random rand;
        
        public Automat(int id)
        {
            rand = new Random();
            Nagrody = new List<Nagroda>();
            ID = id;
            OstatniaWygrana = 0;
            NumerNagrody = 0;
        }

        public void dodajNagrode(Nagroda nagroda)
        {
            Nagrody.Add(nagroda);
        }

        /* ustala zakresy do losowania nagród */
        public void ustalZakresyNagrod()
        {
            int zakres1 = 0, zakres2 = 0;
            foreach (Nagroda nagroda in Nagrody)
            {
                zakres2 += nagroda.Prawdopodobienstwo;
                nagroda.Zakres1 = zakres1;
                nagroda.Zakres2 = zakres2;
                zakres1 += nagroda.Prawdopodobienstwo;
            }
        }

        /* zwraca wartość wygranej */
        /*
        public int zagraj()
        {
            int los = rand.Next(100);
            foreach (Nagroda nagroda in Nagrody)
            {
                if (los >= nagroda.Zakres1 && los < nagroda.Zakres2)
                {
                    OstatniaWygrana = nagroda.Wartosc;
                    return nagroda.Wartosc;
                }
            }
            return 0;
        } */
        public int zagraj()
        {
            OstatniaWygrana = NumerNagrody % Nagrody.Count;
            return Nagrody[(NumerNagrody++) % Nagrody.Count].Wartosc;
        }
    }
}
