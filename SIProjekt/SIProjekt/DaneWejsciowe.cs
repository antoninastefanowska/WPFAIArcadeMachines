using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIProjekt
{
    public class DaneWejsciowe
    {
        public int LiczbaRund { get; set; } /* liczba możliwych zagrań na automatach */
        public int LiczbaAutomatow { get; set; }
        public Automat[] Automaty { get; set; } /* wszystkie automaty */
        
        public DaneWejsciowe()
        {
            /* przykładowe dane do testów (trzeba będzie zrobić wczytywanie z pliku) */
            LiczbaRund = 10;
            LiczbaAutomatow = 6;
            Automaty = new Automat[LiczbaAutomatow];
            for (int i = 0; i < LiczbaAutomatow; i++)
            {
                Automat automat = new Automat(i + 1);
                switch (i)
                {
                    case 0:
                        automat.dodajNagrode(1000);
                        automat.dodajNagrode(100);
                        automat.dodajNagrode(-300);
                        automat.dodajNagrode(-500);
                        automat.dodajNagrode(2000);
                        break;
                    case 1:
                        automat.dodajNagrode(500);
                        automat.dodajNagrode(300);
                        automat.dodajNagrode(-700);
                        automat.dodajNagrode(2000);
                        automat.dodajNagrode(-1000);
                        break;
                    case 2:
                        automat.dodajNagrode(1500);
                        automat.dodajNagrode(-2000);
                        automat.dodajNagrode(2000);
                        automat.dodajNagrode(-3000);
                        automat.dodajNagrode(0);
                        break;
                    case 3:
                        automat.dodajNagrode(100);
                        automat.dodajNagrode(500);
                        automat.dodajNagrode(-500);
                        automat.dodajNagrode(1000);
                        automat.dodajNagrode(100);
                        break;
                    case 4:
                        automat.dodajNagrode(-200);
                        automat.dodajNagrode(0);
                        automat.dodajNagrode(3000);
                        automat.dodajNagrode(0);
                        automat.dodajNagrode(-1000);
                        break;
                    case 5:
                        automat.dodajNagrode(500);
                        automat.dodajNagrode(-1000);
                        automat.dodajNagrode(-1500);
                        automat.dodajNagrode(-100);
                        automat.dodajNagrode(3000);
                        break;
                }
                Automaty[i] = automat;
            }
        }

        private static DaneWejsciowe singleton = null;
        public static DaneWejsciowe Instancja
        {
            get
            {
                if (singleton == null)
                    singleton = new DaneWejsciowe();
                return singleton;
            }
        }
    }
}
