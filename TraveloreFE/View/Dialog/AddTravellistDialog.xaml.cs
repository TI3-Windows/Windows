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
    public sealed partial class AddTravellistDialog : ContentDialog
    {
        TravellistViewModel Tvm;
        Travellist Tl;
        public AddTravellistDialog(TravellistViewModel tvm, Travellist tl)
        {
            this.InitializeComponent();
            Tvm = tvm;
            Tl = tl;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            try
            {
                DateTimeOffset leave;
                DateTimeOffset back;

                try
                {
                    leave = DatePickerLeave.Date.Value;
                    back = DatePickerBack.Date.Value;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Date cannot be empty");
                }

                if (leave > back)
                {
                    throw new ArgumentException("Leave date cannot be after return date");
                }

                Tl.Country = CountryTextBox.Text;
                Tl.DateLeave = new DateTime(leave.Year, leave.Month, leave.Day);
                Tl.DateBack = new DateTime(back.Year, back.Month, back.Day);

                await Tvm.AddNewTravellist(Tl);

            }
            catch (ArgumentException e)
            {
                MessageDialog dialog = new MessageDialog(e.Message);
                await dialog.ShowAsync();
                await this.ShowAsync();
            }

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
