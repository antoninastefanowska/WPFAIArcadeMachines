using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class Nagroda
    {
        public int Wartosc { get; set; }
        public int Prawdopodobienstwo { get; set; }
        public int Zakres1 { get; set; }
        public int Zakres2 { get; set; }

        public Nagroda(int wartosc, int prawdopodobienstwo)
        {
            Wartosc = wartosc;
            Prawdopodobienstwo = prawdopodobienstwo;
        }
    }
}
