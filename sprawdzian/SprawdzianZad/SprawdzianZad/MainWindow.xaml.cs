using Microsoft.Win32;
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


namespace SprawdzianZad
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

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
    



            // Otwórz SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
            };

            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    // Dane do zapisania
                    string imie = dImie.Text;
                    string nazwisko = dNazwisko.Text;
                    string email = dEmail.Text;
                    string plec = (dPlec.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string wiek = (dWiek.SelectedItem as ComboBoxItem)?.Content.ToString();


                    //Wyświetalnie błędu w przypadku złego wpisania Emailu
                    if (!poprawnyEmail(email))
                    {
                        MessageBox.Show("Email jest nieprawidłowy.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Sprawdzanie czy wszystkie pola są uzupełnione
                    if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko) || 
                        string.IsNullOrWhiteSpace(email) || plec == null || wiek == null)
                    {
                        MessageBox.Show("Proszę wypełnić wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //Wzór jak mają być zapisywane dane do TXT
                    string data = $"{imie} {nazwisko}, Email: {email}, [{plec}, {wiek}]";

                    // Zapisywane danych do pliku
                    File.AppendAllText(saveFileDialog.FileName, data + Environment.NewLine);
                    MessageBox.Show("Dane zostały zapisane!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            //Pokazanie błędu
            catch (Exception ex)
            {
                MessageBox.Show("Bład zapisu pliku", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        
        //Metoda sprawdzania poprawnego Emailu
        private bool poprawnyEmail(string email)
        {
            return Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$");
        }
    }
}