using Microsoft.Win32;
using System.IO;
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


namespace Drukarki
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

        private void btnDrukuj_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                FlowDocument dokument = new FlowDocument();
                Paragraph paragraph = new Paragraph(new Run(txtBox.Text));

                dokument.Blocks.Add(paragraph);
                DocumentPaginator paginator = ((IDocumentPaginatorSource)dokument).DocumentPaginator;
                printDialog.PrintDocument(paginator, "Strona do wydruku");


            }
        }

        private void btnLoadRtf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki RTF(*.rtf)|*.rtf|Wszystkie pliki (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using(FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                    {
                        TextRange textRange = new TextRange(rtfTextBox.Document.ContentStart, rtfTextBox.Document.ContentEnd);
                        textRange.Load(fileStream, DataFormats.Rtf);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Problem z wczytaniem pliku {ex.Message}","Błąd odczytu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDrukujRtf_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                try
                {
                    FlowDocument dokument = new FlowDocument();
                    TextRange textRange = new TextRange(rtfTextBox.Document.ContentStart,rtfTextBox.Document.ContentEnd);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        textRange.Save(memoryStream, DataFormats.Xaml);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        TextRange dokRange = new TextRange(dokument.ContentStart,dokument.ContentEnd);
                        dokRange.Load(memoryStream, DataFormats.Xaml);

                        DocumentPaginator paginator = ((IDocumentPaginatorSource)dokument).DocumentPaginator;
                        printDialog.PrintDocument(paginator, "RTF do wydruku");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Problem z wydrukiem : {ex.Message}","Błąd wydruku",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }        
        }
    }
}