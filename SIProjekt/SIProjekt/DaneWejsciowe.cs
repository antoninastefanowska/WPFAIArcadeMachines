using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SIProjekt
{
    public class DaneWejsciowe : INotifyPropertyChanged
    {
        public int LiczbaRund { get; set; } /* liczba możliwych zagrań na automatach */
        public int LiczbaAutomatow { get; set; }
        public ObservableCollection<Automat> Automaty { get; set; } /* wszystkie automaty */
        public int OstatniAutomatID { get; set; }
        
        public DaneWejsciowe()
        {
            Automaty = new ObservableCollection<Automat>();
            LiczbaRund = 10;
        }

        public void WczytajDane(string sciezka)
        {
            Automaty.Clear();

            /* pierwsza linia - liczba rund i liczba automatow */
            StreamReader reader = new StreamReader(sciezka);

            string[] mass = reader.ReadLine().Split(' ');
            LiczbaRund = int.Parse(mass[0]);
            LiczbaAutomatow = int.Parse(mass[1]);

            //kolejne linie - nagrody dla automatow
            for (int i = 0; i < LiczbaAutomatow; i++)
            {
                Automat automat = new Automat(i + 1);
                string[] tmp = reader.ReadLine().Split(' ');
                for (int j = 0; j < tmp.Length; j++)
                {
                    automat.DodajNagrode(int.Parse(tmp[j]));
                }
                Automaty.Add(automat);
            }

            NotifyPropertyChanged("Automaty");
            OstatniAutomatID = Automaty.Count;
        }

        private static DaneWejsciowe singleton = null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

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
