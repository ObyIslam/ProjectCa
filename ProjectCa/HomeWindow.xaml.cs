using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Newtonsoft.Json;



namespace ProjectCa
{
    /// <summary>
    /// Interaction logic for HomWindow.xaml
    /// </summary>
    public partial class HomWindow : Window
    {
        //bob
        static string filePath = "..\\..\\gym.json";
        //static string filePath = @"C:\Users\S00235259\OneDrive - Atlantic TU\Semester 4\ObjectOrientedDevelopment\ProjectCa\ProjectCa\gym.json";
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;
        public HomWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //checks if file exists
            if (!File.Exists(filePath))
            {
                lbx_CommonSearches.ItemsSource = "File not found";
                return;
            }

            try
            {
                string jsonText = File.ReadAllText(filePath);
                //makes it into a readable format
                GymInfo GymInfoWrapper = JsonConvert.DeserializeObject<GymInfo>(jsonText);

                List<string> topics = new List<string>();

                foreach (var gymTopic in GymInfoWrapper.fitness_info)
                {
                    topics.Add(gymTopic.Topic);
                }

                lbx_CommonSearches.ItemsSource = topics;
            }
            catch(Exception ex)
            {
                lbx_CommonSearches.ItemsSource = ex.Message;
            }
        }

        private void lbx_CommonSearches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbx_CommonSearches.SelectedItem != null)
            {
                //Converts the listbox item into a string
                string selectedTopic = lbx_CommonSearches.SelectedItem.ToString();

                string description = "";


                string jsonText = File.ReadAllText(filePath);
                GymInfo GymInfoWrapper = JsonConvert.DeserializeObject<GymInfo>(jsonText);

                foreach (var gymTopic in GymInfoWrapper.fitness_info)
                {
                    //Compares the listbox item
                    if (gymTopic.Topic == selectedTopic)
                    {
                        //gets the description of the selected listbox item
                        description = gymTopic.Description;
                        break;
                    }
                }

                tbx_commonSearches.Text= description;
            }
        }


        private void start_workout_btn_Click(object sender, RoutedEventArgs e)
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
            }
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            TimerDisplay.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

    }
}
