using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TraveloreFE.View.Dialog
{
    public sealed partial class AddDestinationDialog : ContentDialog
    {
        public ItineraryViewModel Ivm;
        public AddDestinationDialog(ItineraryViewModel ivm)
        {
            this.InitializeComponent();
            Ivm = ivm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var date = DatePicker.Date.Value;
            var time = TimePicker.Time;
            var destination = new Destination()
            {
                Name = NameTextBox.Text,
                Street = StreetTextBox.Text,
                Nr = NrTextBox.Text,
                City = CityTextBox.Text,
                Description = DescriptionTextBox.Text,
                VisitTime = new DateTime(date.Year, date.Month, date.Day, time.Hours,time.Minutes, 00)
            };

            Ivm.AddNewDestination(destination);


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
