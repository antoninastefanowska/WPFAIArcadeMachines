using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class Wczytaj : Window
    {
        public Wczytaj()
        {
            InitializeComponent();
        }

        private void Wczytaj_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();

        }

        private void Wczytaj_anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Wartosc_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}