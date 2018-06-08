using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SIProjekt
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Wykres wykres;
        private Podsumowanie oknoPodsumowanie;

        public AlgorytmGenetyczny Algorytm { get; set; }
        public bool Utworzony { get; set; }

        public MainWindow()
        {
            Utworzony = false;
            InitializeComponent();
            Listbox_automaty.ItemsSource = DaneWejsciowe.Instancja.Automaty;
            wykres = new Wykres();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Algorytm = new AlgorytmGenetyczny(5, 100, 1, 3, 80, 10);
        }

        private void Wczytaj_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Pliki tekstowe (.txt)|*.txt";

            if (dlg.ShowDialog() == true)
            {
                string sciezka = dlg.FileName;
                DaneWejsciowe.Instancja.WczytajDane(sciezka);
            }
        }

        private void Wyjdz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Listbox_automaty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listbox_automaty.SelectedItem != null)
            {
                Automat temp = (Automat)Listbox_automaty.SelectedItem;
                Textbox_automat.Text = "Automat " + temp.ID;
                Listbox_nagrody.ItemsSource = temp.Nagrody;
            }
            else
            {
                Textbox_automat.Text = "";
                Listbox_nagrody.ItemsSource = null;
            }
        }

        private void Ustawienia_Click(object sender, RoutedEventArgs e)
        {
            Ustawienia ustawieniaOkno = new Ustawienia();
            ustawieniaOkno.AktualnyAlgorytm = Algorytm;
            ustawieniaOkno.ShowDialog();
        }

        private void Podsumowanie_Click(object sender, RoutedEventArgs e)
        {

            oknoPodsumowanie = new Podsumowanie();
            oknoPodsumowanie.Owner = this;
            oknoPodsumowanie.AktualnyWykres = wykres;
            oknoPodsumowanie.Show();
        }

        private void Krok()
        {
            int x, y, n = wykres.Wartosci.Count;
            Point nowy, ostatni, przedostatni;
            x = Algorytm.NumerIteracji;
            y = Algorytm.NajlepszyOsobnik().Przystosowanie;
            nowy = new Point(x, y);

            wykres.Wartosci.Add(nowy);

            if (n > 0)
            {
                ostatni = wykres.Wartosci[n - 1];
                przedostatni = wykres.Wartosci[n - 2];

                wykres.Zakres = x;
                if (nowy.Y == ostatni.Y && ostatni.Y == przedostatni.Y)
                    wykres.Wartosci.Remove(ostatni);

                n = wykres.Wartosci.Count;
            }
            else wykres.Wartosci.Add(nowy);

            Algorytm.Iteracja();
        }

        private void Button_dodajAutomat_Click(object sender, RoutedEventArgs e)
        {
            Automat automat = new Automat(DaneWejsciowe.Instancja.OstatniAutomatID + 1);
            DaneWejsciowe.Instancja.Automaty.Add(automat);
            DaneWejsciowe.Instancja.LiczbaAutomatow++;
            DaneWejsciowe.Instancja.OstatniAutomatID = automat.ID;
            Resetuj();
        }

        private void Button_usunAutomat_Click(object sender, RoutedEventArgs e)
        {
            Automat automat = Listbox_automaty.SelectedItem as Automat;
            DaneWejsciowe.Instancja.Automaty.Remove(automat);
            DaneWejsciowe.Instancja.LiczbaAutomatow--;
            Resetuj();
        }

        private void Button_dodajNagrode_Click(object sender, RoutedEventArgs e)
        {
            Wczytaj dodajNagrodeOkno = new Wczytaj();
            if (dodajNagrodeOkno.ShowDialog() == true)
            {
                Automat automat = Listbox_automaty.SelectedItem as Automat;
                automat.DodajNagrode(int.Parse(dodajNagrodeOkno.Wartosc_tb.Text));
                Resetuj();
            }
        }

        private void Button_usunNagrode_Click(object sender, RoutedEventArgs e)
        {
            Automat automat = Listbox_automaty.SelectedItem as Automat;
            Nagroda nagroda = Listbox_nagrody.SelectedItem as Nagroda;
            automat.Nagrody.Remove(nagroda);
            Resetuj();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_skokIteracji_Click(object sender, RoutedEventArgs e)
        {
            int n = int.Parse(Skok_tb.Text);
            for (int i = 0; i < n; i++)
                Krok();
            Textbox_przebieg.Text += "Iteracja: " + Algorytm.NumerIteracji.ToString() + "\n" + Algorytm.NajlepszyOsobnik().ToString() + "\n";
            Textbox_przebieg.ScrollToEnd();
        }

        private void Resetuj()
        {
            if (Algorytm.NumerIteracji > 0)
            {
                wykres = new Wykres();
                if (oknoPodsumowanie != null) oknoPodsumowanie.AktualnyWykres = wykres;
                Algorytm.Populacja.Clear();
                Algorytm.NumerIteracji = 0;
                Textbox_przebieg.Text = "";
                DaneWejsciowe.Instancja.ResetujAutomaty();
            }

        }

        private void Rundy_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            DaneWejsciowe.Instancja.LiczbaRund = int.Parse(Rundy_tb.Text);
        }

        private void Utworz_Algorytm_Click(object sender, RoutedEventArgs e)
        {
            Algorytm.GenerujPopulacjePoczatkowa();
            Utworzony = true;
            NotifyPropertyChanged("Utworzony");
        }

        private void Usun_Algorytm_Click(object sender, RoutedEventArgs e)
        {
            Resetuj();
            Utworzony = false;
            NotifyPropertyChanged("Utworzony");
        }
    }
}
