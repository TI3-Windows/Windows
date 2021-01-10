using TraveloreFE.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TraveloreFE.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TraveloreFE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Travellist Travellist { get; set; }
        public User user;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Travellist = (Travellist)e.Parameter;
            mainContent.Navigate(typeof(CategoriesView), Travellist);
            HeaderPaneText.Text = "Categories";
            user = Globals.LoggedInUser;
            this.DataContext = user;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.LoggedInUser = null;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginView));
        }

        private void NavigationViewCategories_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mainContent.Navigate(typeof(CategoriesView), Travellist);
            HeaderPaneText.Text = "Categories";
        }

        private void NavigationViewTasks_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mainContent.Navigate(typeof(TasksView), Travellist);
            HeaderPaneText.Text = "Tasks";
        }

        private void NavigationViewItinerary_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mainContent.Navigate(typeof(ItineraryView), Travellist);
            HeaderPaneText.Text = "Itinerary";
        }

        private void NavigationViewHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(TravellistsView));
        }
    }
}
