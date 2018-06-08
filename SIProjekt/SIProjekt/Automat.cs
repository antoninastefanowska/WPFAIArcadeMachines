using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SIProjekt
{
    public class Automat : INotifyPropertyChanged
    {
        public int ID { get; }
        public ObservableCollection<Nagroda> Nagrody { get; set; }
        public int NumerNastepnejNagrody { get; set; }
        public int OstatniaNagrodaID { get; set; }

        public override string ToString()
        {
            return "Automat " + ID;
        }

        private Random rand;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public Automat(int id)
        {
            Nagrody = new ObservableCollection<Nagroda>();
            ID = id;
            NumerNastepnejNagrody = 0;
            OstatniaNagrodaID = 0;
            rand = new Random();
        }

        public void DodajNagrode(int nagroda)
        {
            Nagrody.Add(new Nagroda(OstatniaNagrodaID + 1, nagroda));
            OstatniaNagrodaID++;
            NotifyPropertyChanged("Nagrody");
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
                return Nagrody[k].Wartosc;
            }
        }
    }
}
