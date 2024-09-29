using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public bool IsSelected { get; set; }

    }
    public partial class MainWindow : Window
    {
        private List<ComboBoxItem> chords;
        private List<ComboBoxItem> activeStrings;
        public MainWindow()
        {
            InitializeComponent();
            InitializeChordDropdown();
            InitializeStringDropdown();
        }

        private void InitializeChordDropdown()
        {
            chords = new List<ComboBoxItem>()
            {
                new ComboBoxItem { Text = "A", IsSelected=false },
                new ComboBoxItem { Text = "C", IsSelected=false },
                new ComboBoxItem { Text = "D", IsSelected=false },
                new ComboBoxItem { Text = "E", IsSelected=false },
                new ComboBoxItem { Text = "G", IsSelected=false },
                new ComboBoxItem { Text = "Am", IsSelected=false },
                new ComboBoxItem { Text = "Dm", IsSelected=false },
                new ComboBoxItem { Text = "Em", IsSelected=false },
            };

            ChordDropDown.ItemsSource = chords;
        }

        public List<ComboBoxItem> GetChords() { return chords; }

        private void InitializeStringDropdown()
        {
            activeStrings = new List<ComboBoxItem>()
            {
                new ComboBoxItem { Text = "High E", IsSelected=true },
                new ComboBoxItem { Text = "B", IsSelected=true },
                new ComboBoxItem { Text = "G", IsSelected=true },
                new ComboBoxItem { Text = "D", IsSelected=true },
                new ComboBoxItem { Text = "A", IsSelected=true },
                new ComboBoxItem { Text = "Low E", IsSelected=true }
            };

            StringDropDown.ItemsSource = activeStrings;
        }

        public List<ComboBoxItem> GetActiveStrings() { return activeStrings; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabsWindow tabsWindow = new TabsWindow(this);
        }

        // Measure Box Event Handlers
        private void Num_Measures_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key) < 34 || ((int)e.Key) > 43)
            {
                e.Handled = true;
            }
        }

        private void Num_Measures_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Num_Measures_Text_Box.Text)) {
                Num_Measures_Text_Box.Text = "14";
            }
        }

        // Fret Box event Handlers
        private void Lowest_Fret_Text_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Lowest_Fret_Text_Box.Text))
            {
                Lowest_Fret_Text_Box.Text = "0";
            }
        }
       
        private void Highest_Fret_Text_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Highest_Fret_Text_Box.Text))
            {
                Highest_Fret_Text_Box.Text = "5";
                e.Handled = true;
                return;
            }

            if (int.Parse(Lowest_Fret_Text_Box.Text) > int.Parse(Highest_Fret_Text_Box.Text))
            {
                Lowest_Fret_Text_Box.Text = Highest_Fret_Text_Box.Text;
            }
        }

        private void Highest_Fret_Text_Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!int.TryParse(e.Text, out int inputNumber))
            {
                e.Handled = true;
                return;
            }

            // Predict what the text will be after the input
            string proposedText = Highest_Fret_Text_Box.Text.Insert(Highest_Fret_Text_Box.SelectionStart, e.Text);

            // Check if the new value would be valid
            if (int.TryParse(proposedText, out int fretValue))
            {
                if (int.TryParse(Lowest_Fret_Text_Box.Text, out int lowFretValue))
                {
                    if (fretValue > 24)
                    {
                        Highest_Fret_Text_Box.Text = 24.ToString();
                        Highest_Fret_Text_Box.SelectionStart = Highest_Fret_Text_Box.Text.Length;
                        e.Handled = true;
                        return;
                    }


                    if (fretValue < lowFretValue && fretValue > 9)
                    {
                        Highest_Fret_Text_Box.Text = lowFretValue.ToString();
                        Highest_Fret_Text_Box.SelectionStart = Highest_Fret_Text_Box.Text.Length; // Set cursor to the end
                        e.Handled = true; // Mark the event as handled to prevent the input
                    }
                }
            }
        }

        private void Lowest_Fret_Text_Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!int.TryParse(e.Text, out int inputNumber))
            {
                e.Handled = true;
                return;
            }

            // Predict what the text will be after the input
            string proposedText = Lowest_Fret_Text_Box.Text.Insert(Lowest_Fret_Text_Box.SelectionStart, e.Text);

            // Check if the new value would be valid
            if (int.TryParse(proposedText, out int fretValue))
            {
                if (int.TryParse(Highest_Fret_Text_Box.Text, out int highFretValue))
                {
                    if (fretValue > highFretValue)
                    {
                        Lowest_Fret_Text_Box.Text = highFretValue.ToString();
                        Lowest_Fret_Text_Box.SelectionStart = Lowest_Fret_Text_Box.Text.Length; // Set cursor to the end
                        e.Handled = true; // Mark the event as handled to prevent the input
                    }
                }
            }
        }

        private void Note_Spacing_Text_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key) < 34 || ((int)e.Key) > 43)
            {
                e.Handled = true;
            }
        }

        private void Note_Spacing_Text_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Note_Spacing_Text_Box.Text))
            {
                Note_Spacing_Text_Box.Text = "3";
            }
        }

        private void Chord_Spacing_Text_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (((int)e.Key) < 34 || ((int)e.Key) > 43)
            {
                e.Handled = true;
            }
        }

        private void Chord_Spacing_Text_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Chord_Spacing_Text_Box.Text))
            {
                Chord_Spacing_Text_Box.Text = "3";
            }
        }

        private void ManualCreateButton_Click(object sender, RoutedEventArgs e)
        {
            ManualWindow manualWindow = new ManualWindow(this, SelectFileNameText.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text Files (*.txt)|*.txt";

            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                string path = fileDialog.FileName;

                SelectFileNameText.Text = path;
            }
            else
            {
                // didn't pick anything
            }
        }
    }
}
