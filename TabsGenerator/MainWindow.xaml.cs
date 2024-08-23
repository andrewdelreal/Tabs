using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            myText.Text = "Text";
            myText.Foreground = Brushes.Blue;

            TextBlock myTB = new TextBlock();
            myTB.Text = "Text";
            myTB.Inlines.Add(" more text to use. Trying to make overflows");
            myTB.Inlines.Add(new Run(" Run text that I added in backend")
            {
                Foreground = Brushes.Red,
                TextDecorations = TextDecorations.Underline
            });
            myTB.TextWrapping = TextWrapping.Wrap;
            this.Content = myTB;
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (randomCheck.IsChecked == true)
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
    }

    public partial class TabsWindow : Window
    {
        private Grid grid;
        private Random rand;
        private MainWindow _MainWindow;

        public TabsWindow(MainWindow MainWindow)
        {
            rand = new Random();
            this._MainWindow = MainWindow;

            setUpGrid();
            this.Content = grid;
            this.WindowState = WindowState.Maximized;
            this.Show();
        }

        private string GenerateTabs()
        {
            string retString = "";
            const int NOTE_SPACE = 3;
            const int NUM_STRINGS = 6;
            const int MAX_MEASURES_PER_LINE = 14;
            const int NUM_BEATS = 4;

            int NUM_MEASURES = int.Parse(_MainWindow.Num_Measures_Text_Box.Text);
            // Add strings to list for for loop iteration
            string [] stringHeaders = new string[NUM_STRINGS];
            stringHeaders[0] = "E|-";
            stringHeaders[1] = "B|-";
            stringHeaders[2] = "G|-";
            stringHeaders[3] = "D|-";
            stringHeaders[4] = "A|-";
            stringHeaders[5] = "E|-";

            int measures_left = NUM_MEASURES;
            while (measures_left > 0) // while there are measures left to print
            {
                // for each line of music
                int measures_this_line = MAX_MEASURES_PER_LINE;
                if (measures_left < MAX_MEASURES_PER_LINE)
                    measures_this_line = measures_left;    // To adjust for the last remaining measures

                string measures = new string('-', NOTE_SPACE * NUM_BEATS * measures_this_line); // Get the right string lengths
                string[] strings = new string[NUM_STRINGS];

                for (int i = 0; i < NUM_STRINGS; i++)
                {
                    strings[i] = "" + measures;
                }

                strings = RandomNotes(strings, measures_this_line); // will need to adjust to call the correct function

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

        //Come ip with a way to update any string at any time/ Maybe have a list of string builders, and dont loop through the strings
        private string[] RandomNotes(string[] strings, int measures_this_line)
        {
            const int NUM_STRINGS = 6;
            const int NOTE_SPACE = 3;
            const int NUM_BEATS = 4;


            int LOWEST_FRET = int.Parse(_MainWindow.Lowest_Fret_Text_Box.Text);
            int HIGHEST_FRET = int.Parse(_MainWindow.Highest_Fret_Text_Box.Text);
            int NUM_MEASURES = measures_this_line;

            string[] retStrings = new string[NUM_STRINGS];


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
                retStrings[i] = sbs[i].ToString();
            }

            return retStrings;
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
