using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TraveloreFE.Model;
using TraveloreFE.View.Dialog;
using TraveloreFE.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TraveloreFE.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItineraryView : Page
    {

        public Destination selectedDestionation;
        public Travellist Travellist;
        public ItineraryViewModel Ivm;

        public ItineraryView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Travellist = (Travellist)e.Parameter;
            DataContext = new ItineraryViewModel(Travellist, lvDestinations);
            Ivm = (ItineraryViewModel)DataContext;
        }

        private async void btnAddDestination_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddDestinationDialog(Ivm);
            await dialog.ShowAsync();
         
        }

        private void lvDestinations_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listView = (ListView)sender;
            travellistFlyouts.ShowAt(listView, e.GetPosition(listView));
            var a = ((FrameworkElement)e.OriginalSource).DataContext;
            selectedDestionation = (Destination)a;
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDestionation != null)
            {
                Ivm.DeleteDestinationCommand.Execute(selectedDestionation.Id);
            }
        }
        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDestionation != null)
            {
                UpdateDestinationDialog dialog = new UpdateDestinationDialog(Ivm, selectedDestionation);
                await dialog.ShowAsync();
            }
        }

    }
}
