using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class Nagroda
    {
        public int ID { get; set; }
        public int Wartosc { get; set; }

        public Nagroda(int id, int wartosc)
        {
            ID = id;
            Wartosc = wartosc;
        }

        public override string ToString()
        {
            return "Nagroda " + ID.ToString() + ": " + Wartosc.ToString() + " zł"; 
        }
    }
}
