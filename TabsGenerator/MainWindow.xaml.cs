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
                TabsWindow tabsWindow = new TabsWindow();
            }
        }


    }

    public partial class TabsWindow : Window
    {
        private Grid grid;
        private string GenerateTabs()
        {
            string retString = "";
            const int NOTE_SPACE = 3;
            const int NUM_STRINGS = 6;
            const int NUM_MEASURES = 10;
            const int NUM_BEATS = 4;

            string highE = "E|-";
            string B = "B|-";
            string G = "G|-";
            string D = "D|-";
            string A = "A|-";
            string lowE = "E|-";

            // Add strings to list for for loop iteration
            string [] strings = new string[NUM_STRINGS];
            strings[0] = highE;
            strings[1] = B;
            strings[2] = G;
            strings[3] = D;
            strings[4] = A;
            strings[5] = lowE;

            // initialize strings
            // will need to change for measures

            // Creating measures
            string blank_measure = "|" + new string(' ', (NOTE_SPACE * NUM_BEATS + 1));
            string measure = "|" + new string('-', NOTE_SPACE * NUM_BEATS + 1);

            string initial_string = "";
            string string_spacing = "";

            for (int i = 0; i < NUM_MEASURES; i++)
            {
                initial_string += measure;
                string_spacing += blank_measure;
            }
            initial_string += "|";
            string_spacing += "|";


            for (int i = 0; i < NUM_STRINGS; i++)
            {
                strings[i] += initial_string;
            }


            strings = RandomNotes(strings);
            for (int i = 0; i < NUM_STRINGS; i++)
            {

                retString += "\n" + strings[i] + "\n";
                if (i != NUM_STRINGS - 1)
                    retString += " | " + string_spacing;
            }
            
            return retString;
        }

        //Come ip with a way to update any string at any time/ Maybe have a list of string builders, and dont loop through the strings
        private string[] RandomNotes(string[] strings)
        {
            const int NUM_STRINGS = 6;
            const int LOWEST_FRET = 0;
            const int HIGHEST_FRET = 5;
            const int NOTE_SPACE = 3;
            const int NUM_MEASURES = 10;
            const int NUM_BEATS = 4;

            string[] retStrings = new string[NUM_STRINGS];

            // + 2 is to skip the "|-" at the beginning of each measure
            const string measure_buffer = "|-";
            int measure_length = NUM_BEATS * NOTE_SPACE + measure_buffer.Length;

            for (int i = 0; i < NUM_STRINGS; i++)
            {
                StringBuilder sb = new StringBuilder(strings[i]);
                // skips beginning of string (the part containing E|-|-)
                int currentIdx = 5;

                // loop runs based on how many dashes there are with out the leading "|-"
                for (int j = NOTE_SPACE; j <= NUM_BEATS * NOTE_SPACE * NUM_MEASURES; j += NOTE_SPACE)
                {
                    sb[currentIdx] = '0'; 
                    currentIdx += NOTE_SPACE;
                    if (j % ((NUM_BEATS) * NOTE_SPACE) == 0 && j != 0)
                    {
                        currentIdx += measure_buffer.Length;
                    }
                }
                retStrings[i] = sb.ToString();
            }

            return retStrings;
        } 

        public TabsWindow() 
        {
            setUpGrid();
            this.Show();
            this.Content = grid;
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
                    FontSize = 16
                }
            };

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
