using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SIProjekt
{
    public class Automat
    {
        public int ID { get; }
        public List<int> Nagrody { get; set; }
        public int NumerNastepnejNagrody { get; set; }
        public Brush Kolor { get; set; }

        private Random rand;
        
        public Automat(int id)
        {
            Nagrody = new List<int>();
            ID = id;
            NumerNastepnejNagrody = 0;
            rand = new Random();
            Kolor = new SolidColorBrush(Color.FromRgb((byte)rand.Next(100), (byte)rand.Next(100), (byte)rand.Next(100)));
        }

        public void DodajNagrode(int nagroda)
        {
            Nagrody.Add(nagroda);
        }

        /* zwraca wartość wygranej */
        public int Zagraj()
        {
            if (Nagrody.Count == 0) return 0;
            else
            {
                int k;
                k = NumerNastepnejNagrody;
                NumerNastepnejNagrody = (NumerNastepnejNagrody + 1) % Nagrody.Count;
                return Nagrody[k];
            }
        }
    }
}
