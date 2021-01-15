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
        public Category selectedCategory;
        public string lastSelectedType;
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
            var aantalItems = 0;
            var aantalItemsDone = 0;
            foreach (Category cat in cvm.Categories)
            {
                aantalItems += cat.Items.Count;
                foreach (Item i in cat.Items)
                {
                    if (i.DoneItem)
                    {
                        aantalItemsDone++;
                    }
                }
            }
            progressbar.Maximum = aantalItems;
            progressbar.Value = aantalItemsDone;
            double percentageDone = 100;
            if (aantalItems != 0)
            {
                percentageDone = progressbar.Value / progressbar.Maximum * 100;
            }

            ItemsDone.Text = Math.Round(percentageDone).ToString() + "%";
        }

        private async void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                progressbar.Value--;
                var percentageDone = progressbar.Value / progressbar.Maximum * 100;
                ItemsDone.Text = Math.Round(percentageDone).ToString() + "%";
            }
            else
            {
                progressbar.Value++;
                var percentageDone = progressbar.Value / progressbar.Maximum * 100;
                ItemsDone.Text = Math.Round(percentageDone).ToString() + "%";
            }
            string itemName = cb.Content.ToString();
            await cvm.UpdateItem(itemName);
        }

        private async void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog md;
            if (cvm.Categories.Count != 0)
            {
                var name = Naam.Text;
                var amount = 1;
                var categoryName = "";
                try
                {
                    amount = Convert.ToInt32(Amount.Text);
                }
                catch (FormatException)
                {
                    md = new MessageDialog("Enter a valid number");
                    await md.ShowAsync();
                }

                if(cmbCategory.SelectedItem != null)
                {
                    categoryName = cmbCategory.SelectedItem.ToString();
                }
                else
                {
                    md = new MessageDialog("You have to select a category!");
                }

                if (Naam.Text.Length != 0)
                {
                    if (Amount.Text.Length != 0)
                    {
                        if (amount > 0)
                        {
                            if (cmbCategory.SelectedItem != null)
                            {
                                await cvm.AddNewItem(name, amount, categoryName);
                                Naam.Text = "";
                                Amount.Text = "";
                                LvCat.ItemsSource = null;
                                LvCat.ItemsSource = cvm.Categories;
                                progressbar.Maximum++;
                                var percentageDone = progressbar.Value / progressbar.Maximum * 100;
                                ItemsDone.Text = Math.Round(percentageDone).ToString() + "%";
                            }
                            else
                            {
                                md = new MessageDialog("Please select a category.");
                                await md.ShowAsync();
                            }
                        }
                        else
                        {
                            md = new MessageDialog("Amount can't be less than or equal to 0!");
                            await md.ShowAsync();
                        }
                    }
                    else
                    {
                        md = new MessageDialog("Amount can't be empty!");
                        await md.ShowAsync();
                    }
                }
                else
                {
                    md = new MessageDialog("Name can't be empty!");
                    await md.ShowAsync();
                }
            }
            else
            {
                md = new MessageDialog("You can't add an item without a category!");
                await md.ShowAsync();
            }
        }

        private async void BtnAddCat_Click(object sender, RoutedEventArgs e)
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


        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            if(lastSelectedType != null && !lastSelectedType.Equals(""))
            {
                if (lastSelectedType.Equals("Category") && selectedCategory != null)
                {
                    await cvm.RemoveCat(selectedCategory);
                }
                else
                {
                    if(selectedItem != null)
                    {
                        await cvm.RemoveItem(selectedItem);
                        LvCat.ItemsSource = null;
                        LvCat.ItemsSource = cvm.Categories;
                    }
                }
            }

        }

        private void LvCat_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listView = (ListView)sender;
            CategoryMenuFlyout.ShowAt(listView, e.GetPosition(listView));
            var a = ((FrameworkElement)e.OriginalSource).DataContext;
            if (a.GetType().Name.Equals("Item"))
            {
                selectedItem = (Item)a;
                lastSelectedType = "Item";
            }
            else if(a.GetType().Name.Equals("Category"))
            {
                
                selectedCategory = (Category)a;
                lastSelectedType = "Category";
            }
        }
    }
}
