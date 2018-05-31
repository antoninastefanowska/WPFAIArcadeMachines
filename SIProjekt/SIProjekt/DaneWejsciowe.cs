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
        public List<Automat> Automaty { get; set; } /* wszystkie automaty */
        
        public DaneWejsciowe()
        {
            /* przykładowe dane do testów (trzeba będzie zrobić wczytywanie z pliku) */
            LiczbaRund = 10;
            LiczbaAutomatow = 6;
            Automaty = new List<Automat>();
            for (int i = 0; i < LiczbaAutomatow; i++)
            {
                Automat automat = new Automat(i + 1);
                switch (i)
                {
                    case 0:
                        automat.DodajNagrode(1000);
                        automat.DodajNagrode(100);
                        automat.DodajNagrode(-300);
                        automat.DodajNagrode(-500);
                        automat.DodajNagrode(2000);
                        break;
                    case 1:
                        automat.DodajNagrode(500);
                        automat.DodajNagrode(300);
                        automat.DodajNagrode(-700);
                        automat.DodajNagrode(2000);
                        automat.DodajNagrode(-1000);
                        break;
                    case 2:
                        automat.DodajNagrode(1500);
                        automat.DodajNagrode(-2000);
                        automat.DodajNagrode(2000);
                        automat.DodajNagrode(-3000);
                        automat.DodajNagrode(0);
                        break;
                    case 3:
                        automat.DodajNagrode(100);
                        automat.DodajNagrode(500);
                        automat.DodajNagrode(-500);
                        automat.DodajNagrode(1000);
                        automat.DodajNagrode(100);
                        break;
                    case 4:
                        automat.DodajNagrode(-200);
                        automat.DodajNagrode(0);
                        automat.DodajNagrode(3000);
                        automat.DodajNagrode(0);
                        automat.DodajNagrode(-1000);
                        break;
                    case 5:
                        automat.DodajNagrode(500);
                        automat.DodajNagrode(-1000);
                        automat.DodajNagrode(-1500);
                        automat.DodajNagrode(-100);
                        automat.DodajNagrode(3000);
                        break;
                }
                Automaty.Add(automat);
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

        public void ResetujAutomaty()
        {
            foreach (Automat automat in Automaty)
                automat.NumerNastepnejNagrody = 0;
        }
    }
}
