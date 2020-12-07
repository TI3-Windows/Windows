using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using TraveloreFE.Model;
using TraveloreFE.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class CategoriesView : Page
    {
        public Item selectedItem;
        public CategoriesViewModel cvm;
        public Travellist Travellist { get; set; }
        public CategoriesView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Travellist = (Travellist)e.Parameter;
            DataContext = new CategoriesViewModel(Travellist);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //private async void Update_Click(object sender, RoutedEventArgs e)
        //{
        //    if (selectedCategory != null)
        //    {
        //        await cvm.UpdateSelectedCategory(selectedCategory.Id);
        //    }
        //}
    }
}
