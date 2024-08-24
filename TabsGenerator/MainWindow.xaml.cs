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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public bool IsSelected { get; set; }

    }
    public partial class MainWindow : Window
    {
        private List<ComboBoxItem> chords;
        public MainWindow()
        {
            InitializeComponent();
            InitializeChordDropdown();
        }

        private void InitializeChordDropdown()
        {
            chords = new List<ComboBoxItem>()
            {
                new ComboBoxItem { Text = "A", IsSelected=false },
                new ComboBoxItem { Text = "B", IsSelected=false },
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

        public List<ComboBoxItem> GetChords() {  return chords; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (randomCheck.IsChecked == true || chordCheck.IsChecked == true)
            {
                TabsWindow tabsWindow = new TabsWindow(this);
            }
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
                if (int.Parse(Lowest_Fret_Text_Box.Text) > int.Parse(Highest_Fret_Text_Box.Text))
                {
                    Lowest_Fret_Text_Box.Text = Highest_Fret_Text_Box.Text;
                }
                return;
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
    }

    public partial class TabsWindow : Window
    {
        private Grid grid;
        private Random rand;
        private MainWindow _MainWindow;
        private Dictionary<string, string> chordStrings;
        private string[] stringHeaders;

        public TabsWindow(MainWindow MainWindow)
        {
            rand = new Random();
            this._MainWindow = MainWindow;
            stringHeaders = new string[6];
            stringHeaders[0] = "E|-";
            stringHeaders[1] = "B|-";
            stringHeaders[2] = "G|-";
            stringHeaders[3] = "D|-";
            stringHeaders[4] = "A|-";
            stringHeaders[5] = "E|-";

            InitializeChordDictionary();
            setUpGrid();
            this.Content = grid;
            this.WindowState = WindowState.Maximized;
            this.Show();
        }

        private string GenerateTabs()
        {
            string retString = "";
                
            if (_MainWindow.randomCheck.IsChecked == true && _MainWindow.chordCheck.IsChecked == true)
            {

            }
            else if (_MainWindow.randomCheck.IsChecked == true)
            {
                retString = NotesString();

            }
            else
            {
                retString = ChordsString();
            }

            return retString;
        }

        //Come ip with a way to update any string at any time/ Maybe have a list of string builders, and dont loop through the strings
        private string[] RandomNotes(int measures_this_line)
        {
            const int NUM_STRINGS = 6;
            const int NUM_BEATS = 4;

            int NOTE_SPACE = int.Parse(_MainWindow.Note_Spacing_Text_Box.Text);
            int LOWEST_FRET = int.Parse(_MainWindow.Lowest_Fret_Text_Box.Text);
            int HIGHEST_FRET = int.Parse(_MainWindow.Highest_Fret_Text_Box.Text);
            int NUM_MEASURES = measures_this_line;


            //string measures = new string('-', NOTE_SPACE * NUM_BEATS * measures_this_line); // Get the right string lengths
            string[] strings = new string[NUM_STRINGS];

            for (int i = 0; i < NUM_STRINGS; i++)
            {
                strings[i] = new string('-', NOTE_SPACE * NUM_BEATS * measures_this_line);
            }


            // make list of string builders for random note generation
            StringBuilder[] sbs = new StringBuilder[NUM_STRINGS];
            for (int i = 0; i < NUM_STRINGS; i++)
            {
                sbs[i] = new StringBuilder(strings[i]);
            }
            
            for (int i = 0; i < NUM_BEATS * NUM_MEASURES; i++)
            {
                int stringIdx = rand.Next(0, NUM_STRINGS); // get string to randomize
                int fretInt = rand.Next(LOWEST_FRET, HIGHEST_FRET + 1); // get random fret from fret range

                // change this eventually for slide and gliss notes and stuff
                
                char fretChar = fretInt.ToString().ToCharArray()[0];

                sbs[stringIdx][i * NOTE_SPACE] = fretChar;

                if (fretInt > 9)
                {
                    fretChar = fretInt.ToString().ToCharArray()[1];
                    sbs[stringIdx][i * NOTE_SPACE + 1] = fretChar;
                }
            }

            for (int i = 0; i < NUM_STRINGS; i++)
            {
                strings[i] = sbs[i].ToString();
            }

            return strings;
        } 

        private string NotesString()
        {
            string retString = "";
            const int MAX_SPACES = 170;
            const int NUM_STRINGS = 6;
            const int NUM_BEATS = 4;

            int NOTE_SPACE = int.Parse(_MainWindow.Note_Spacing_Text_Box.Text);
            int MAX_MEASURES_PER_LINE = MAX_SPACES / (NOTE_SPACE * NUM_BEATS);
            int NUM_MEASURES = int.Parse(_MainWindow.Num_Measures_Text_Box.Text);

            int measures_left = NUM_MEASURES;
            while (measures_left > 0) // while there are measures left to print
            {
                // for each line of music
                int measures_this_line = MAX_MEASURES_PER_LINE;
                if (measures_left < MAX_MEASURES_PER_LINE)
                    measures_this_line = measures_left;    // To adjust for the last remaining measures

                string[] strings;

                strings = RandomNotes(measures_this_line);
                // Complete each string line
                for (int i = 0; i < NUM_STRINGS; i++)
                {
                    strings[i] = stringHeaders[i] + strings[i] + "|";
                }

                // concatenate strings to one large string to print
                for (int i = 0; i < NUM_STRINGS; i++)
                {
                    retString += "\n" + strings[i];
                }
                retString += "\n";

                measures_left -= MAX_MEASURES_PER_LINE;
            }
            return retString;
        }

        private string[] RandomChords(int measures_this_line)
        {
            const int NUM_STRINGS = 6;
            const int NUM_BEATS = 4;

            int NUM_MEASURES = measures_this_line;
            int NOTE_SPACE = int.Parse(_MainWindow.Chord_Spacing_Text_Box.Text);

            string[] strings = new string[NUM_STRINGS];

            for (int i = 0; i < NUM_STRINGS; i++)
            {
                strings[i] = new string('-', NOTE_SPACE * NUM_BEATS * measures_this_line);
            }

            // make list of string builders for random note generation
            StringBuilder[] sbs = new StringBuilder[NUM_STRINGS];
            for (int i = 0; i < NUM_STRINGS; i++)
            {
                sbs[i] = new StringBuilder(strings[i]);
            }

            List<ComboBoxItem> allChords = _MainWindow.GetChords();

            List<ComboBoxItem> selectedChords = new List<ComboBoxItem>();
            foreach (ComboBoxItem chord in allChords) {
                if (chord.IsSelected == true)
                {
                    selectedChords.Add(chord);
                }
            }

            int numChords = selectedChords.Count;

            for (int i = 0; i < NUM_BEATS * NUM_MEASURES; i++)
            {
                if (numChords != 0)
                {
                    string key = selectedChords[rand.Next(0, numChords)].Text;
                    string chord = chordStrings[key];
                    for (int k = 0; k < NUM_STRINGS; k++)
                    {
                        // if chord string char is a number
                        if (chord[k].ToString() != "x")
                        {
                            sbs[k][i * NOTE_SPACE] = chord[k];
                            /*
                            if (k == 0)
                            {
                                char[] chordName = key.ToCharArray();
                                int idx = 1;
                                foreach (char c in chordName)
                                {
                                    sbs[k][i * NOTE_SPACE + idx] = c;
                                    idx++;
                                }
                                
                            }
                            */
                        }
                    }
                }
            }

            for (int i = 0; i < NUM_STRINGS; i++)
            {
                strings[i] = sbs[i].ToString();
            }

            return strings;
        }

        private string ChordsString()
        {
            string retString = "";
            const int MAX_SPACES = 170;
            const int NUM_STRINGS = 6;
            const int NUM_BEATS = 4;

            int NOTE_SPACE = int.Parse(_MainWindow.Chord_Spacing_Text_Box.Text);
            int MAX_MEASURES_PER_LINE = MAX_SPACES / (NOTE_SPACE * NUM_BEATS);
            int NUM_MEASURES = int.Parse(_MainWindow.Num_Measures_Text_Box.Text);

            int measures_left = NUM_MEASURES;
            while (measures_left > 0) // while there are measures left to print
            {
                // for each line of music
                int measures_this_line = MAX_MEASURES_PER_LINE;
                if (measures_left < MAX_MEASURES_PER_LINE)
                    measures_this_line = measures_left;    // To adjust for the last remaining measures

                // move string initialization to proper methods
                string[] strings;

                strings = RandomChords(measures_this_line);
                // Complete each string line
                for (int i = 0; i < NUM_STRINGS; i++)
                {
                    strings[i] = stringHeaders[i] + strings[i] + "|";
                }

                // concatenate strings to one large string to print
                for (int i = 0; i < NUM_STRINGS; i++)
                {
                    retString += "\n" + strings[i];
                }
                retString += "\n";

                measures_left -= MAX_MEASURES_PER_LINE;
            }
            return retString;
        }

        void InitializeChordDictionary()
        {
            chordStrings = new Dictionary<string, string>
            {
                {"A", "02220x"} 
            };
        }

        // Set up Tablature grid
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
            TextBlock tb1 = new TextBlock()
            {
                Text = "Tablature",
                FontSize = 50,
                FontFamily = new FontFamily("Times New Roman"),
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };


            // Tabs Viewer
            ScrollViewer scrollViewer = new ScrollViewer()
            {
                Content = new TextBlock
                {
                    Text = GenerateTabs(),
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Courier New"),
                    FontSize = 18,
                    Margin = new Thickness{Left=10.0, Right=0, Top=0,Bottom=0}
                }
            };

            // Footer Text Box
            TextBlock tb3 = new TextBlock()
            {
                Text = "Bottom Text",
                FontFamily = new FontFamily("Times New Roman")
            };

            Grid.SetRow(tb1, 0);
            Grid.SetRow(scrollViewer, 1);
            Grid.SetRow(tb3, 2);

            grid.Children.Add(tb1);
            grid.Children.Add(scrollViewer);
            grid.Children.Add(tb3);
            grid.ShowGridLines = true;

            this.Content = grid;
        }
    }
}
