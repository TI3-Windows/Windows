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

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Travellist = (Travellist)e.Parameter;
            mainContent.Navigate(typeof(CategoriesView), Travellist);
        }

        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        { 
            if(args.InvokedItem.Equals("Categories"))
            {
                mainContent.Navigate(typeof(CategoriesView), Travellist);
            }
            if(args.InvokedItem.Equals("Tasks"))
            {
                mainContent.Navigate(typeof(TasksView), Travellist);
            }
            if(args.InvokedItem.Equals("Map"))
            {
                mainContent.Navigate(typeof(MapRoute));
            }
            if (args.InvokedItem.Equals("Travellist"))
            {
                mainContent.Navigate(typeof(TravellistsView));
            }
            if (args.InvokedItem.Equals("Itinerary"))
            {
                mainContent.Navigate(typeof(ItineraryView), Travellist);
            }
            if (args.InvokedItem.Equals("Account"))
            {
                mainContent.Navigate(typeof(AccountView));
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.LoggedInUser = null;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginView));
        }
    }
}
