using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.ObjectModel;

namespace Activity_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    // Enum for the activity types
    public enum ActivityType { Air, Water, Land }

    //Activity class using icomparable to sort through list.
    public class Activity : IComparable
    {

        #region Properties
        //Name of Activites
        public string Name { get; set; }
        public string _description;

        //Long hand property getting the description and cost
        public string Description
        {
            get
            {
                return string.Format($"{_description} Cost - {Cost:c}");
            }
            set
            {
                _description = value;
            }
        }
        /// <summary>
        // Date of Activities
        /// </summary>
        public DateTime ActivityDate { get; set; }
        //Cost of activities
        public decimal Cost { get; set; }
        //Access to the enum
        public ActivityType TypeOfActivity { get; set; }

        #endregion Properties

        #region Methods

        //Method with icomparable, used to sort the Activities in list by day.
        public int CompareTo(object obj)
        {
            Activity that = (Activity)obj;
            return ActivityDate.CompareTo(that.ActivityDate);
        }
        //Override string to display the information in the constructors for activity class along with formatting the string
        public override string ToString()
        {
                                            //Format the date to display as shown in string
            return string.Format($"{Name} - {ActivityDate.ToString("dd'/'MM'/'yyyy")}");
        }

        #endregion Methods

    }
    //Window class & code
    public partial class MainWindow : Window
    {
        // Lists to store activities
        List<Activity> activities;
        List<Activity> chosen;
        List<Activity> filtered;

        //variable to track running total
        decimal totalCost = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        //excutes the code once the window is finished loading
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //assigns the label text to the Total Cost variable
            LblMoney.Content = $"{totalCost:c}";

            //initializing the Activity constructors to generate objects
            Activity l1 = new Activity()
            {
                Name = "Treking",
                Description = "Instructor led group trek through local mountains.",
                ActivityDate = new DateTime(2019, 06, 01),
                TypeOfActivity = ActivityType.Land,
                Cost = 20m
            };

            Activity l2 = new Activity()
            {
                Name = "Mountain Biking",
                Description = "Instructor led half day mountain biking.  All equipment provided.",
                ActivityDate = new DateTime(2019, 06, 02),
                TypeOfActivity = ActivityType.Land,
                Cost = 30m
            };

            Activity l3 = new Activity()
            {
                Name = "Abseiling",
                Description = "Experience the rush of adrenaline as you descend cliff faces from 10-500m.",
                ActivityDate = new DateTime(2019, 06, 03),
                TypeOfActivity = ActivityType.Land,
                Cost = 40m
            };

            Activity w1 = new Activity()
            {
                Name = "Kayaking",
                Description = "Half day lakeland kayak with island picnic.",
                ActivityDate = new DateTime(2019, 06, 01),
                TypeOfActivity = ActivityType.Water,
                Cost = 40m
            };

            Activity w2 = new Activity()
            {
                Name = "Surfing",
                Description = "2 hour surf lesson on the wild atlantic way",
                ActivityDate = new DateTime(2019, 06, 02),
                TypeOfActivity = ActivityType.Water,
                Cost = 25m
            };

            Activity w3 = new Activity()
            {
                Name = "Sailing",
                Description = "Full day lakeland kayak with island picnic.",
                ActivityDate = new DateTime(2019, 06, 03),
                TypeOfActivity = ActivityType.Water,
                Cost = 50m
            };

            Activity a1 = new Activity()
            {
                Name = "Parachuting",
                Description = "Experience the thrill of free fall while you tandem jump from an airplane.",
                ActivityDate = new DateTime(2019, 06, 01),
                TypeOfActivity = ActivityType.Air,
                Cost = 100m
            };

            Activity a2 = new Activity()
            {
                Name = "Hang Gliding",
                Description = "Soar on hot air currents and enjoy spectacular views of the coastal region.",
                ActivityDate = new DateTime(2019, 06, 02),
                TypeOfActivity = ActivityType.Air,
                Cost = 80m
            };

            Activity a3 = new Activity()
            {
                Name = "Helicopter Tour",
                Description = "Experience the ultimate in aerial sight-seeing as you tour the area in our modern helicopters",
                ActivityDate = new DateTime(2019, 06, 03),
                TypeOfActivity = ActivityType.Air,
                Cost = 200m
            };

            //creating the lists for the activities to display in the list box

            //list for available item
            activities = new List<Activity>();

            //list for selected item
            chosen = new List<Activity>();

            //list for filtered items
            filtered = new List<Activity>();

            //add the Activity objects to the activities list
            activities.Add(l1);
            activities.Add(l2);
            activities.Add(l3);
            activities.Add(w1);
            activities.Add(w2);
            activities.Add(w3);
            activities.Add(a1);
            activities.Add(a2);
            activities.Add(a3);

            //sorting the list by day
            activities.Sort();
            chosen.Sort();

            //chosing the source for display for the listbox
            //setting the items to be from the activties list
            LstChoices.ItemsSource = activities;
            LstChosen.ItemsSource = chosen;

        }

        //executes once an item in the listbox is selected
        private void LstChoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try catch for null errors.
            try
            {
                //assigns the report(underneath description) label text to be the description of the selected item in listbox
                LblReport.Content = $"{((Activity)LstChoices.SelectedItem).Description}";
            }
            //catches null exception
            catch (NullReferenceException)
            {
                Write("Null value");
            }
        }

        //excutes once the add button is clicked (>>)
        private void BtnRAdd_Click(object sender, RoutedEventArgs e)
        {
            //boolean to check for conflicting dates
            bool dateCheck = false;
            try
            {
                //checks if an item/activity has been selected
                if (LstChoices.SelectedItem != null)
                {
                    //checks each item in the list
                    foreach (Activity item in chosen)
                    {
                        //compares selected item date to the already added items date
                        if (item.ActivityDate == ((Activity)LstChoices.SelectedItem).ActivityDate)
                        {
                            //assigns variable to true
                            dateCheck = true;
                        }
                    }
                    //if the dateCheck is true displays message warning user the dates are conflicting
                    if (dateCheck == true)
                    {
                        //message to user and resets the date check
                        MessageBox.Show("Conflicting date");
                        dateCheck = false;
                    }
                    else
                    {
                        //if the datecheck is false will increase and display the total cost
                        totalCost += ((Activity)LstChoices.SelectedItem).Cost;
                        LblMoney.Content = $"{totalCost:c}";

                        //remove the selected item/object from the list and adds it to the selected/chosen list
                        activities.Remove((Activity)LstChoices.SelectedItem);
                        chosen.Add((Activity)LstChoices.SelectedItem);

                        //refreshes the screen to display the selected and available activities
                        Update();
                    }
                }
                else
                {
                    //if no item is selected this message will be displayed
                    MessageBox.Show("No activity selected");
                }
            }
            catch (NullReferenceException)
            {
                Write("Null value");
            }
            

        }

        //excutes when the (<<) remove button is clicked
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //checks if item is selected
                if (LstChosen.SelectedItem != null)
                {
                    //decreases and displays the total cost
                    totalCost -= ((Activity)LstChosen.SelectedItem).Cost;
                    LblMoney.Content = $"{totalCost:c}";

                    //adds and removes the item from and to the correct lists
                    activities.Add((Activity)LstChosen.SelectedItem);
                    chosen.Remove((Activity)LstChosen.SelectedItem);

                    //refreshes screen
                    Update();
                }
                else
                {
                    MessageBox.Show("No activity selected");
                }
            }
            catch (NullReferenceException)
            {
                Write("Null value");
            }
            
        }

        //executes on a selected item in the right/selected activity list box
        private void LstChosen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //updates the report label to the description of the selected item
                LblReport.Content = $"{((Activity)LstChosen.SelectedItem).Description}";
            }
            catch (NullReferenceException)
            {
                Write("Null value");
            }
        }

        //updates the display/listbox for the user
        private void Update()
        {
            //sets listbox sources to null to clear the listbox
            LstChosen.ItemsSource = null;

            //sorts the list after the addition and removal of new items
            chosen.Sort();

            //sets the new source for the display for the lsit box
            LstChosen.ItemsSource = chosen;
            LstChoices.ItemsSource = null;
            activities.Sort();
            LstChoices.ItemsSource = activities;
        }

        //executes on click on radio buttons, handles all radio buttons
        private void RdoAll_Click(object sender, RoutedEventArgs e)
        {
            //clears the filtered lsit
            filtered.Clear();

            // checks if radio button is checked
            if (RdoAll.IsChecked == true)
            {
                //refresh screen
                Update();
            }
            //all else if statements check which radio button is selected
            else if (RdoLand.IsChecked == true)
            {
                //runs through each Activity object in the activites list to check to type
                foreach (Activity activity in activities)
                {
                    //comapres the type of activity in the list to the enum land
                    if (activity.TypeOfActivity == ActivityType.Land)
                    {
                        //adds the activity to the filtered list
                        filtered.Add(activity);

                        //refreshes the listbox for the user
                        LstChoices.ItemsSource = null;
                        LstChoices.ItemsSource = filtered;
                    }
                }
            }
            //same process as statement above
            else if (RdoWater.IsChecked == true)
            {
                foreach (Activity activity in activities)
                {
                    if (activity.TypeOfActivity == ActivityType.Water)
                    {
                        filtered.Add(activity);
                        LstChoices.ItemsSource = null;
                        LstChoices.ItemsSource = filtered;
                    }
                }
            }
            else if (RdoAir.IsChecked == true)
            {
                foreach (Activity activity in activities)
                {
                    if (activity.TypeOfActivity == ActivityType.Air)
                    {
                        filtered.Add(activity);
                        LstChoices.ItemsSource = null;
                        LstChoices.ItemsSource = filtered;
                    }
                }
            }
        }
    }
}
