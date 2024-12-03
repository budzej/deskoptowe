using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;

namespace Paszporty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Zdarzenie click na przycisku OK
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            string imie = txtImie.Text;
            string nazwisko = txtNazwisko.Text;
            string kolor ="";

            //Regexy
            string ptrnTekst = @"^[A-Z][a-z]*$";
            Regex regexTekst = new Regex(ptrnTekst);

            if (!regexTekst.IsMatch(imie) || !regexTekst.IsMatch(nazwisko))
            {
                MessageBox.Show("Wprowadź poprawne dane");
                return;
            }

            if (rdNiebieskie.IsChecked == true) {
                kolor = "niebieskie";
            }
            if (rdZielone.IsChecked == true)
            {
                kolor = "zielone";
            }
            if (rdPiwne.IsChecked == true)
            {
                kolor = "piwne";
            }

            if (String.IsNullOrEmpty(imie) || String.IsNullOrEmpty(nazwisko))
            {
                MessageBox.Show("Wprowadź dane");
                return;
            }

            MessageBox.Show(imie + " " + nazwisko + " kolor oczu " + kolor);


        }


        //Wyjście z pola edycyjnego numer
        private void txtNumer_LostFocus(object sender, RoutedEventArgs e)
        {
            
            string numer = txtNumer.Text;
            string source = "C:\\Users\\t4\\Documents\\kody\\deskoptowe\\deskoptowe\\Paszporty\\Paszporty\\bin\\Debug\\net8.0-windows\\materialy\\";
            string zdjecie = source + numer + "-zdjecie.jpg";
            string odcisk = source + numer + "-odcisk.jpg";

            if (!File.Exists(zdjecie) || !File.Exists(odcisk))
            {
                imgZdjecie.Source = null;
                imgOdcisk.Source = null;
                return;
            }
            
            BitmapImage zdjecie1 = new BitmapImage();
            zdjecie1.BeginInit();
            zdjecie1.UriSource = new Uri(zdjecie, UriKind.RelativeOrAbsolute);
            zdjecie1.EndInit();

            BitmapImage odcisk1 = new BitmapImage();
            odcisk1.BeginInit();
            odcisk1.UriSource = new Uri(odcisk, UriKind.RelativeOrAbsolute);
            odcisk1.EndInit();


            imgZdjecie.Source = zdjecie1;
            imgOdcisk.Source = odcisk1;


        }

        private void chkProporcje_Checked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Stretch = Stretch.Uniform;
            imgOdcisk.Stretch = Stretch.Uniform;
        }
        private void chkProporcje_unchecked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Stretch = Stretch.Fill;
            imgOdcisk.Stretch = Stretch.Fill;
        }

        private void chkPrzez_Checked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Opacity = 1;
            imgOdcisk.Opacity = 1;
        }
        private void chkPrzez_unChecked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Opacity = 0.5;
            imgOdcisk.Opacity = 0.5;
        }
    }
}