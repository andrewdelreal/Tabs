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
            _MainWindow = MainWindow;
            stringHeaders = new string[6];
            stringHeaders[0] = "E|-";
            stringHeaders[1] = "B|-";
            stringHeaders[2] = "G|-";
            stringHeaders[3] = "D|-";
            stringHeaders[4] = "A|-";
            stringHeaders[5] = "E|-";

            InitializeChordDictionary();
            setUpGrid();
            WindowState = WindowState.Maximized;
            Show();
        }

        private string GenerateTabs()
        {
            string retString = "";

            if (_MainWindow.BothNotesAndChordsCheck.IsChecked == true)
            {
                retString = NotesAndChordsString();
                // For combining both, I could make the actual note placement/chord placement a method
                // to individually add a note. So I could call a proportion of notes to chords
            }
            else if (_MainWindow.RandomNotesCheck.IsChecked == true)
            {
                retString = NotesString();
            }
            else if (_MainWindow.RandomChordsCheck.IsChecked == true)
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

            string[] strings = new string[NUM_STRINGS];

            for (int i = 0; i < NUM_STRINGS; i++)
            {
                strings[i] = new string('-', NOTE_SPACE * NUM_BEATS * measures_this_line);
            }

            // make list of string builders for random note generation

            List<StringBuilder> sbs = new List<StringBuilder>();
            List<int> list_of_active_strings = new List<int>();
            List<ComboBoxItem> activeStrings = _MainWindow.GetActiveStrings();
            for (int i = 0; i < NUM_STRINGS; i++)
            {
                sbs.Add(new StringBuilder(strings[i]));
                if (activeStrings[i].IsSelected == true)
                {
                    list_of_active_strings.Add(i); // add idx of string if it selected in the main window
                }
            }

            for (int i = 0; i < NUM_BEATS * NUM_MEASURES; i++)
            {
                int fretInt = rand.Next(LOWEST_FRET, HIGHEST_FRET + 1); // get random fret from fret range

                InsertNote(ref sbs, ref list_of_active_strings, fretInt, NOTE_SPACE, i);
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
            List<StringBuilder> sbs = new List<StringBuilder>();
            for (int i = 0; i < NUM_STRINGS; i++)
            {
                sbs.Add(new StringBuilder(strings[i]));
            }

            List<ComboBoxItem> allChords = _MainWindow.GetChords();

            List<ComboBoxItem> selectedChords = new List<ComboBoxItem>();
            foreach (ComboBoxItem chord in allChords)
            {
                if (chord.IsSelected == true)
                {
                    selectedChords.Add(chord);
                }
            }

            int numChords = selectedChords.Count;
            if (numChords != 0)
            {
                for (int i = 0; i < NUM_BEATS * NUM_MEASURES; i++)
                {
                    InsertChord(ref sbs, ref selectedChords, NOTE_SPACE, i);
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

        void InsertNote(ref List<StringBuilder> sbs, ref List<int> list_of_active_strings, int fretInt, int NOTE_SPACE, int i)
        {
            int stringIdx = rand.Next(0, list_of_active_strings.Count); // get string to randomize

            char fretChar = fretInt.ToString().ToCharArray()[0];

            sbs[list_of_active_strings[stringIdx]][i * NOTE_SPACE] = fretChar;

            // add second character for numbers greater than one character in length
            if (fretInt > 9)
            {
                fretChar = fretInt.ToString().ToCharArray()[1];
                sbs[stringIdx][i * NOTE_SPACE + 1] = fretChar;
            }
        }

        void InsertChord(ref List<StringBuilder> sbs, ref List<ComboBoxItem> list_of_active_chords, int NOTE_SPACE, int i)
        {
            const int NUM_STRINGS = 6;
            int numChords = list_of_active_chords.Count;
            string key = list_of_active_chords[rand.Next(0, numChords)].Text;
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
                            
                        }*/
                }
            }
        }

        string NotesAndChordsString()
        {
            // create ratio of notes to chords
            string retString = "";
            const int MAX_SPACES = 170;
            const int NUM_STRINGS = 6;
            const int NUM_BEATS = 4;

            int NOTE_SPACE = int.Parse(_MainWindow.Note_Spacing_Text_Box.Text);
            int CHORD_SPACE = int.Parse(_MainWindow.Chord_Spacing_Text_Box.Text);
            int MAX_MEASURES_PER_LINE = MAX_SPACES / (NOTE_SPACE * NUM_BEATS);
            int NUM_MEASURES = int.Parse(_MainWindow.Num_Measures_Text_Box.Text);

            double note_multiplier = 0.5;
            double chord_multiplier = 0.5;

            int measures_left = NUM_MEASURES;
            while (measures_left > 0) // while there are measures left to print
            {
                // then based on list or before make the calculation for measures thi line
                // Need to make num_notes based on the num of beats and the combined max
                // dived note space
                int note_spaces = (int)(note_multiplier * (double)MAX_SPACES);
                int chord_spaces = (int)(chord_multiplier * (double)MAX_SPACES);

                int num_notes = note_spaces / (NOTE_SPACE * NUM_BEATS);
                int num_chords = chord_spaces / (CHORD_SPACE * NUM_BEATS);
                // add the list of function calls 
                List<string> functions = new List<string>();   
                for (int i = 0; i < num_notes; i++)
                {
                    functions.Add("note");
                }
                for (int i = 0; i < num_chords; i++)
                {
                    functions.Add("chord");
                }
                Shuffle(functions);
                // dont use old max measures per line
                // for each line of music
                int measures_this_line = MAX_MEASURES_PER_LINE;
                if (measures_left < MAX_MEASURES_PER_LINE)
                    measures_this_line = measures_left;    // To adjust for the last remaining measures

                // move string initialization to proper methods
                string[] strings;
                for (int i = 0; i < functions.Count(); i++)
                {
                    Console.WriteLine(functions[i]);
                }

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

        string[] RandomNotesAndChords(int measures_this_line)
        {
            const int NUM_STRINGS = 6;

            int NUM_MEASURES = measures_this_line;
            int NOTE_SPACE = int.Parse(_MainWindow.Chord_Spacing_Text_Box.Text);

            string[] strings = new string[NUM_STRINGS];
            return strings;
        }

        static void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

        void InitializeChordDictionary()
        {
            chordStrings = new Dictionary<string, string>
            {
                { "A", "02220x" },
                { "C", "01023x" },
                { "D", "2320xx" },
                { "E", "001220" },
                { "G", "330023" },
                { "Am", "01220x" },
                { "Dm", "1320xx" },
                { "Em", "000220" }
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
                    Margin = new Thickness { Left = 10.0, Right = 0, Top = 0, Bottom = 0 }
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

            Content = grid;
        }
    }
}
