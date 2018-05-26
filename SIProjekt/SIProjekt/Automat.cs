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
        public List<int> Nagrody { get; set; }
        public int NumerAktualnejNagrody { get; set; }
        
        public Automat(int id)
        {
            Nagrody = new List<int>();
            ID = id;
            NumerAktualnejNagrody = 0;
        }

        public void DodajNagrode(int nagroda)
        {
            Nagrody.Add(nagroda);
        }

        /* zwraca wartość wygranej */
        public int Zagraj()
        {
            int k;
            k = NumerAktualnejNagrody;
            NumerAktualnejNagrody = (NumerAktualnejNagrody + 1) % Nagrody.Count;
            return Nagrody[k];
        }
    }
}
