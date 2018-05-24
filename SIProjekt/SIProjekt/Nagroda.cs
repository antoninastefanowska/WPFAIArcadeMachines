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
        public int Prawdopodobienstwo { get; set; } /* prawdopodobieństwo wylosowania tej nagrody w procentach */
        public int Zakres1 { get; set; } /* dolna granica do losowania */
        public int Zakres2 { get; set; } /* górna granica do losowania */

        public Nagroda(int wartosc, int prawdopodobienstwo)
        {
            Wartosc = wartosc;
            Prawdopodobienstwo = prawdopodobienstwo;
        }
    }
}
