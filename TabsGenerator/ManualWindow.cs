using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TabsGenerator
{
    public partial class ManualWindow : Window
    {
        private MainWindow _MainWindow;
        private Grid grid;
        private Button saveButton;
        private string[] stringHeaders;
        private TextBox tabs;
        private TextBox fileTitle;
        public ManualWindow(MainWindow mainWindow, string text)
        {

            _MainWindow = mainWindow;

            stringHeaders = new string[6];
            stringHeaders[0] = "E|-";
            stringHeaders[1] = "B|-";
            stringHeaders[2] = "G|-";
            stringHeaders[3] = "D|-";
            stringHeaders[4] = "A|-";
            stringHeaders[5] = "E|-";

            tabs = new TextBox();
            setUpGrid();
            WindowState = WindowState.Maximized;
            Show();
        }

        void setUpGrid()
        {
            grid = new Grid();
            // make the grid rows
            RowDefinition row1 = new RowDefinition { Height = new GridLength(0.1, GridUnitType.Star) };
            RowDefinition row2 = new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) };
            RowDefinition row3 = new RowDefinition { Height = new GridLength(0.1, GridUnitType.Star) };

            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);

            // Title Text
            // change text of title to just file name.
            string fileName = System.IO.Path.GetFileName(_MainWindow.SelectFileNameText.Text);

            fileTitle = new TextBox()
            {
                Text = fileName,
                FontSize = 50,
                FontFamily = new FontFamily("Times New Roman"),
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = Brushes.Transparent
            };

            // Tabs Viewer
            ScrollViewer scrollViewer = new ScrollViewer()
            {
                Content = tabs
            };

            tabs.Text = LoadTabs();
            tabs.FontWeight = FontWeights.Bold;
            tabs.FontFamily = new FontFamily("Courier New");
            tabs.FontSize = 18;
            tabs.Margin = new Thickness { Left = 10.0, Right = 0, Top = 0, Bottom = 0 };

            tabs.PreviewKeyDown += TextBox_PreviewKeyDown;
            tabs.PreviewTextInput += TextBox_PreviewTextInput;


            // Footer Text Box
            saveButton = new Button()
            {
                Content = "Save",
                FontFamily = new FontFamily("Times New Roman")
            };

            saveButton.Click += new RoutedEventHandler(SaveButton_Click);


            Grid.SetRow(fileTitle, 0);
            Grid.SetRow(scrollViewer, 1);
            Grid.SetRow(saveButton, 2);

            grid.Children.Add(fileTitle);
            grid.Children.Add(scrollViewer);
            grid.Children.Add(saveButton);
            grid.ShowGridLines = true;

            Content = grid;
        }

        // This is only here to handle the backspace key, and change it to a '-' insertion instead

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Get the TextBox
            var textBox = sender as TextBox;

            // Handle Backspace to replace the previous character with '-'
            if (e.Key == Key.Back)
            {
                int caretIndex = textBox.CaretIndex;

                if (caretIndex > 0)
                {
                    textBox.Text = textBox.Text.Remove(caretIndex - 1, 1).Insert(caretIndex - 1, "-");

                    textBox.CaretIndex = caretIndex - 1;

                    e.Handled = true;
                }
            }

            // ignore spaces
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // This method will handle all insertions for any characters or symbols to allow users to enter notes or marks, without changing the format of the page.

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            string newText = e.Text;

            if (!string.IsNullOrEmpty(newText) && IsValidInput(newText))
            {
                // Get current caret position
                int caretIndex = textBox.CaretIndex;

                // Replace character at caret position or append at the end
                if (caretIndex < textBox.Text.Length)
                {
                    textBox.Text = textBox.Text.Remove(caretIndex, 1).Insert(caretIndex, newText);
                }
                else
                {
                    textBox.Text += newText;
                }

                // Move caret forward by one
                textBox.CaretIndex = caretIndex + 1;

                // Mark the event as handled (prevents default input behavior)
                e.Handled = true;
            }
        }

        // This methoed is to ignore control inputs
        private bool IsValidInput(string input)
        {
            // Allow all printable characters, excluding control characters
            return !string.IsNullOrEmpty(input) && input.All(c => !char.IsControl(c));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string docPath = "";
            try
            {
                // Combine the path and file name.
                string filePath;

                using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                {
                    var result = fbd.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        filePath = fbd.SelectedPath;
                        filePath += "/" + fileTitle.Text;
                        Console.WriteLine(filePath);
                        File.WriteAllText(filePath, tabs.Text);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("You do not have permission to write to the file: WriteFile.txt");
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("The directory does not exist: " + docPath);
            }
            catch (IOException)
            {
                throw new Exception("An I/O error occurred while writing to the file: WriteFile.txt");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred: " + ex.Message);
            }
        }

        private string LoadTabs()
        {
            string retString = "";

            if (_MainWindow.SelectFileNameText.Text == "New File")
            {
                retString = NewTabs();
            }
            else
            {
                string path = _MainWindow.SelectFileNameText.Text;
                try
                {
                    retString = File.ReadAllText(path);
                }
                catch (FileNotFoundException)
                {
                    throw new Exception("The file was not found: " + path);
                }
                catch (UnauthorizedAccessException)
                {
                    throw new Exception("You do not have permission to access the file: " + path);
                }
                catch (IOException)
                {
                    throw new Exception("An I/O error occurred while reading the file: " + path);
                }
            }

            return retString;
        }

        private string NewTabs()
        {
            string retString = "";

            const int NUM_SPACES = 170;

            for (int k = 0; k < 3; k++)
            {
                for (int i = 0; i < 6; i++)
                {
                    string currString = stringHeaders[i];
                    currString += new string('-', NUM_SPACES);
                    currString += "|";
                    retString += currString + "\n";
                }
                retString += "\n";
            }

            return retString;
        }

    }
}
