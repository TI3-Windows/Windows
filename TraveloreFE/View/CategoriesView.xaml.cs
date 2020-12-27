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
            cvm = (CategoriesViewModel)DataContext;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {


        }

        private async void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var name = Naam.Text;
            var amount = 1;
            try
            {
                amount = Convert.ToInt32(Amount.Text);
            }
            catch (FormatException)
            {
                MessageDialog md = new MessageDialog("Enter a valid number");
                await md.ShowAsync();
            }
            var categoryName = cmbCategory.SelectedItem.ToString();
            if (Naam.Text.Length != 0)
            {
                if (Amount.Text.Length != 0)
                {
                    if (amount != 0)
                    {
                        if (cmbCategory.SelectedItem != null)
                        {
                            await cvm.AddNewItem(name, amount, categoryName);
                            Naam.Text = "";
                            Amount.Text = "";
                            lvCat.ItemsSource = null;
                            lvCat.ItemsSource = cvm.Categories;
                        }
                        else
                        {
                            MessageDialog md = new MessageDialog("Please select a category.");
                            await md.ShowAsync();
                        }
                    }
                    else
                    {
                        MessageDialog md = new MessageDialog("Amount can't be 0!");
                        await md.ShowAsync();
                    }
                }
                else
                {
                    MessageDialog md = new MessageDialog("Amount can't be empty!");
                    await md.ShowAsync();
                }
            }
            else
            {
                MessageDialog md = new MessageDialog("Name can't be empty!");
                await md.ShowAsync();
            }
        }

        private async void btnAddCat_Click(object sender, RoutedEventArgs e)
        {
            var name = NaamCat.Text;
            if (NaamCat.Text.Length != 0)
            {
                await cvm.AddNewCat(name);
                NaamCat.Text = "";
            }
            else
            {
                MessageDialog md = new MessageDialog("Name can't be empty");
                await md.ShowAsync();
            }
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
