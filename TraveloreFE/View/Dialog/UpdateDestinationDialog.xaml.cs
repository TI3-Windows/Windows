using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TraveloreFE.View.Dialog
{
    public sealed partial class UpdateDestinationDialog : ContentDialog
    {
        public ItineraryViewModel Ivm;
        public Destination Destination;
        public UpdateDestinationDialog(ItineraryViewModel ivm, Destination destination)
        {
            this.InitializeComponent();
            Destination = destination;
            Ivm = ivm;
           
            NameTextBox.Text = Destination.Name;
            StreetTextBox.Text = Destination.Street;
            NrTextBox.Text = Destination.Nr;
            CityTextBox.Text = Destination.City;
            DescriptionTextBox.Text = Destination.Description;
            DatePicker.Date = Destination.VisitTime.Date;
            TimePicker.Time = Destination.VisitTime.TimeOfDay;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                DateTimeOffset date;
                TimeSpan time;
                try
                {
                    date = DatePicker.Date.Value;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Date cannot be empty");
                }

                try
                {
                    time = TimePicker.Time;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Time cannot be empty");
                }

                var destination = new Destination()
                {
                    Name = NameTextBox.Text,
                    Street = StreetTextBox.Text,
                    Nr = NrTextBox.Text,
                    City = CityTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    VisitTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 00)
                };
                await Ivm.UpdateSelectedDestination(Destination.Id ,destination);
                //lvDestinations.ItemsSource = null;
                //lvDestinations.ItemsSource = Ivm.Itinerary;
            }
            catch (ArgumentException ex)
            {
                MessageDialog md = new MessageDialog(ex.Message.ToString());
                await md.ShowAsync();
                await this.ShowAsync();
            }
            catch (Exception e)
            {

            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
