using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIProjekt
{
    /// <summary>
    /// Interaction logic for UstawDane.xaml
    /// </summary>
    public partial class Wczytaj : Window
    {
        public Wczytaj()
        {
            InitializeComponent();
        }

        private void Wczytaj_ok_Click(object sender, RoutedEventArgs e)
        {
            //tak np mozna sie dostac do tej wartosci tekstowej
            ((MainWindow)Application.Current.MainWindow).Nazwa = Nazwa_pliku_tb.Text;
            this.Close();

        }

        private void Wczytaj_anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}