using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace ParallelFileProcessing
{
    public partial class MainWindow : Window
    {
        private string sourceDirectory;
        private string targetDirectory;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Wybór folderu źródłowego
        private void btnSelectSourceFolder_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder",
            };

            // Pokazanie okna dialogowego
            if (folderDialog.ShowDialog() == true)
            {
                sourceDirectory = Path.GetDirectoryName(folderDialog.FileName);
                txtOutput.AppendText($"Wybrano folder źródłowy: {sourceDirectory}\n");
            }
        }

        // Wybór folderu docelowego
        private void btnSelectTargetFolder_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder",
            };

            if (folderDialog.ShowDialog() == true)
            {
                targetDirectory = Path.GetDirectoryName(folderDialog.FileName);
                txtOutput.AppendText($"Wybrano folder docelowy: {targetDirectory}\n");
            }
        }

        // Rozpoczęcie przetwarzania plików - wielkie litery
        private async void btnProcessFiles_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sourceDirectory) || string.IsNullOrEmpty(targetDirectory))
            {
                MessageBox.Show("Proszę wybrać folder źródłowy i docelowy.");
                return;
            }

            var files = Directory.GetFiles(sourceDirectory, "*.txt");
            if (files.Length == 0)
            {
                MessageBox.Show("Brak plików do przetworzenia w wybranym folderze.");
                return;
            }

            txtOutput.AppendText($"Rozpoczynanie przetwarzania {files.Length} plików...\n");

            // Przetwarzanie plików równolegle
            await ProcessFilesParallel(files, ToUpperCase);
        }

        // Rozpoczęcie przetwarzania plików - małe litery
        private async void btnProcessFilesLower_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(sourceDirectory) || string.IsNullOrEmpty(targetDirectory))
            {
                MessageBox.Show("Proszę wybrać folder źródłowy i docelowy.");
                return;
            }

            var files = Directory.GetFiles(sourceDirectory, "*.txt");
            if (files.Length == 0)
            {
                MessageBox.Show("Brak plików do przetworzenia w wybranym folderze.");
                return;
            }

            txtOutput.AppendText($"Rozpoczynanie przetwarzania {files.Length} plików...\n");

            // Zmiana czcionki w TextBox dla tego przycisku
            txtOutput.FontStyle = System.Windows.FontStyles.Italic;

            // Przetwarzanie plików równolegle
            await ProcessFilesParallel(files, ToLowerCase);
        }

        // Równoległe przetwarzanie plików
        private async Task ProcessFilesParallel(string[] files, Func<string, string> transformFunction)
        {
            var tasks = files.Select(file => ProcessFile(file, transformFunction)).ToArray();
            await Task.WhenAll(tasks);

            txtOutput.AppendText("Przetwarzanie zakończone!\n");
        }

        // Przetwarzanie pojedynczego pliku
        private async Task ProcessFile(string filePath, Func<string, string> transformFunction)
        {
            try
            {
                // Odczytywanie zawartości pliku
                string content = await File.ReadAllTextAsync(filePath);

                // Przekształcenie tekstu
                string transformedContent = transformFunction(content);

                // Ścieżka do zapisanego pliku w katalogu docelowym
                string targetFilePath = Path.Combine(targetDirectory, Path.GetFileName(filePath));

                // Zapisywanie przetworzonego pliku
                await File.WriteAllTextAsync(targetFilePath, transformedContent);

                // Dodawanie informacji do TextBox
                Dispatcher.Invoke(() =>
                {
                    txtOutput.AppendText($"Plik przetworzony: {Path.GetFileName(filePath)}\n");
                });
            }
            catch (Exception ex)
            {
                // Obsługa wyjątków
                Dispatcher.Invoke(() =>
                {
                    txtOutput.AppendText($"Błąd podczas przetwarzania pliku {Path.GetFileName(filePath)}: {ex.Message}\n");
                });
            }
        }

        // Funkcja do konwersji na wielkie litery
        private string ToUpperCase(string content)
        {
            return content.ToUpper();
        }

        // Funkcja do konwersji na małe litery
        private string ToLowerCase(string content)
        {
            return content.ToLower();
        }
    }
}
