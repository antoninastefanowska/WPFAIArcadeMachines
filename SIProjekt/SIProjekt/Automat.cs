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

        private Random rand;
        
        public Automat(int id)
        {
            rand = new Random();
            Nagrody = new List<Nagroda>();
            ID = id;
        }

        public void dodajNagrode(Nagroda nagroda)
        {
            Nagrody.Add(nagroda);
        }

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

        public int zagraj()
        {
            int los = rand.Next(100);
            Console.Write(los.ToString() + ' ');
            foreach (Nagroda nagroda in Nagrody)
            {
                if (los >= nagroda.Zakres1 && los < nagroda.Zakres2)
                    return nagroda.Wartosc;
            }
            return 0;
        }
    }
}
