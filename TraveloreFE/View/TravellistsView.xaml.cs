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
    public sealed partial class TravellistsView : Page
    {
        //public User User { get; set; }
        //public UserViewModel uvm;
        private Travellist selectedTravellist;
        public TravellistViewModel tvm;

        public TravellistsView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //User = (User)e.Parameter;
            //DataContext = new TravellistViewModel(User);
            //uvm = (UserViewModel)DataContext;
            DataContext = new TravellistViewModel();
            tvm = (TravellistViewModel)DataContext;
        }

        private async void gvTravellist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            selectedTravellist = (Travellist)gvTravellists.SelectedItem;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage), selectedTravellist);
        }

        private async void addTravellist_Click(object sender, RoutedEventArgs e)
        {

            var travellist = TravellistName.Text;
            if (TravellistName.Text.Length != 0)
            {
                Travellist tl = new Travellist(travellist);
                AddTravellistDialog dialog = new AddTravellistDialog(tvm, tl);
                await dialog.ShowAsync();
                //await tvm.AddNewTravellist(travellist);
                TravellistName.Text = "";
                gvTravellists.ItemsSource = null;
                gvTravellists.ItemsSource = tvm.Travellists;
            }
            else
            {
                MessageDialog md = new MessageDialog("The title can't be empty!");
                await md.ShowAsync();
            }
        }

        private void lvTravellists_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            GridView listView = (GridView)sender;
            travellistFlyouts.ShowAt(listView, e.GetPosition(listView));
            var a = ((FrameworkElement)e.OriginalSource).DataContext;
            selectedTravellist = (Travellist)a ;
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTravellist != null)
            {
                tvm.DeleteTravellistCommand.Execute(selectedTravellist.Id);
                gvTravellists.ItemsSource = null;
                gvTravellists.ItemsSource = tvm.Travellists;
            }
        }
    }
}
