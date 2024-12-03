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
using System.Text.RegularExpressions;

namespace regexy
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

        private void btnKodPocztowy_Click(object sender, RoutedEventArgs e)
        {
            string ptrnKodpocztowy = @"^\d{2}-\d{3}$";
            string inputKodpocztowy = txtBoxKodPocztowy.Text;

            bool isValidKod = Regex.IsMatch(inputKodpocztowy, ptrnKodpocztowy);

            if (isValidKod)
            {
                MessageBox.Show("Kod jest poprawny", "Sprawdzanie kodu", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Kod jest niepoprawny", "Błąd kodu", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            string ptrnEmail = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$";
            string ptrnEmail2 = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string inputEmail = txtBoxEmail.Text;

            Regex regex = new Regex(ptrnEmail2);
            bool isValidEmail = regex.IsMatch(inputEmail);
            if (isValidEmail)
            {
                MessageBox.Show("Email jest poprawny", "Sprawdzanie emaila", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Email jest niepoprawny", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnNumeryTel_Click(object sender, RoutedEventArgs e)
        {
            string ptrnTelefony = @"\d{3}-\d{3}-\d{3}";
            string numeryTelefonow = txtBoxNumeryTel.Text;
            Regex regexTel = new Regex(ptrnTelefony);
            MatchCollection matches = regexTel.Matches(numeryTelefonow);
            foreach (Match m in matches)
            {
                MessageBox.Show($"{m}", "Numer tel.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnDate_Click(object sender, RoutedEventArgs e)
        {
            string prnDate = @"(\d{4})-(\d{2})-(\d{2})";
            string replaceDate = "$3-$2-$1";
            string txtDate = txtBoxDate.Text;

            Regex regexDate = new Regex(prnDate);
            string newDate = regexDate.Replace(txtDate, replaceDate);

            MessageBox.Show($"Nowa data: {newDate}", "Formatownie date", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnUsuwanie_Click(object sender, RoutedEventArgs e)
        {
            string ptrnZdanie = @"\s+";
            string txtZdanie = txtBoxZdanie.Text;

            Regex regexZdanie = new Regex(ptrnZdanie);
            string newZdanie = regexZdanie.Replace(txtZdanie, " ");

            MessageBox.Show($"{newZdanie}", "Usuwanie nadmiernych spacji", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnPodmien_Click(object sender, RoutedEventArgs e)
        {
            string ptrnZdanie2 = @"tulipan|róża|goździk|hortensja";
            string txtZdanie2 = txtBoxZdanie2.Text;

            Regex regexZdanie2 = new Regex(ptrnZdanie2);
            string newZdanie2 = regexZdanie2.Replace(txtZdanie2, "kwiatek");

            MessageBox.Show($"Nowe zdanie: {newZdanie2}", "Podmiana", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnWymien_Click(object sender, RoutedEventArgs e)
        {
            string ptrnZdanie3 = @",";
            string txtZdanie3 = txtBoxZdanie3.Text;

            Regex regexZdanie3 = new Regex(ptrnZdanie3);
            string[] jezyki = regexZdanie3.Split(txtZdanie3);

            foreach (string jezyk in jezyki)
            {
                MessageBox.Show($"{jezyk}", "Języki", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}