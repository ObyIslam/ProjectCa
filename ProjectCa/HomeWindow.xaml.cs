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
        static string filePath2 = "..\\..\\workout.json";
        //static string filePath = @"C:\Users\S00235259\OneDrive - Atlantic TU\Semester 4\ObjectOrientedDevelopment\ProjectCa\ProjectCa\gym.json";
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;
        static List<string> workouts = new List<string>();
        private List<ExcerciseInfo> exercises;
        private List<GymTopic> GymTopics;
        public HomWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if workout JSON file exists
            if (!File.Exists(filePath2))
            {
                display_excercises_lbx.ItemsSource = new List<string> { "File not found" };
                return;
            }

            try
            {
                // Deserialize gym JSON
                string jsonText = File.ReadAllText(filePath);
                GymInfo gymInfoWrapper = JsonConvert.DeserializeObject<GymInfo>(jsonText);

                List<string> topics = new List<string>();
                foreach (var gymTopic in gymInfoWrapper.fitness_info)
                {
                    topics.Add(gymTopic.Topic);
                }
                lbx_CommonSearches.ItemsSource = topics;

                // Deserialize workout JSON
                string jsonText2 = File.ReadAllText(filePath2);
                Excercises exerciseData = JsonConvert.DeserializeObject<Excercises>(jsonText2);

                //setup list for searchiing topics
                GymTopics = gymInfoWrapper.fitness_info;

                //setup list for excerises for searching
                exercises = exerciseData.exercises; 

                display_excercises_lbx.ItemsSource = exercises.Select(exercise => exercise.name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                display_excercises_lbx.ItemsSource = new List<string> { "An error occurred. Please try again later." };
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

                tbx_commonSearches.Text = description;
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

        private void add_excerise_btn_Click(object sender, RoutedEventArgs e)
        {
            if (display_excercises_lbx.SelectedItem != null)
            {
                workouts.Add(display_excercises_lbx.SelectedItem.ToString());
                lbx_workouts.ItemsSource = null;
                lbx_workouts.ItemsSource = workouts;
            }
        }

        private void remove_excercise_Click(object sender, RoutedEventArgs e)
        {
            if(lbx_workouts.SelectedItem != null)
            {
                workouts.Remove(lbx_workouts.SelectedItem.ToString());
                lbx_workouts.ItemsSource = null;
                lbx_workouts.ItemsSource= workouts;
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = searchtbx.Text.ToLower(); // Convert search text to lower case for case-insensitive search
            if (exercises != null)
            {
                List<ExcerciseInfo> filteredExercises = exercises.Where(exercise => exercise.name.ToLower().Contains(searchText)).ToList();
                display_excercises_lbx.ItemsSource = filteredExercises.Select(exercise => exercise.name);
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string searchText = topicstbx.Text.ToLower(); // Convert search text to lower case for case-insensitive search
            if (GymTopics != null)
            {
                List<GymTopic> filteredExercises = GymTopics.Where(exercise => exercise.Topic.ToLower().Contains(searchText)).ToList();
                lbx_CommonSearches.ItemsSource = filteredExercises.Select(exercise => exercise.Topic);
            }
        }

        private void LoadVideo(string videoUrl)
        {
            videoWebBrowser.Navigate(videoUrl);
        }

        private void WatchVideo_Click(object sender, RoutedEventArgs e)
        {
            ExcerciseInfo exercise = (sender as Button).DataContext as ExcerciseInfo;
            LoadVideo(exercise.video);
        }


        // I dont know why this doesnt work, it doesnt recognise the object
        private void lbx_workouts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbx_workouts.SelectedItem != null)
                {
                    ExcerciseInfo selectedExercise = (ExcerciseInfo)lbx_workouts.SelectedItem;
                    if (selectedExercise != null && !string.IsNullOrEmpty(selectedExercise.video))
                    {
                        LoadVideo(selectedExercise.video);
                    }
                    else
                    {
                        MessageBox.Show("No video available for this exercise.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the video: {ex.Message}");
            }
        }


    }
}
