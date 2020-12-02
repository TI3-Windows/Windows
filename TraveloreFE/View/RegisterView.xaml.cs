using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TraveloreFE.Model;
using TraveloreFE.ViewModel;
using Windows.ApplicationModel.UserActivities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Control;
using Windows.System.UserProfile;
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
    public sealed partial class RegisterView : Page
    {
        public UserViewModel uvm;

        public RegisterView()
        {
            this.InitializeComponent();
            DataContext = new UserViewModel();
            uvm = (UserViewModel)DataContext;
        }

        private void loginViewBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginView));
        }

        private async void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Password;
            string confirmPassword = tbConfirmPassword.Password;
            string firstname = tbFirstname.Text;
            string lastname = tbLastname.Text;

            User user = await uvm.RegisterUser(email, password, confirmPassword, firstname, lastname);
            if(user == null)
            {
                MessageDialog md = new MessageDialog("Username already exists");
                await md.ShowAsync();
                return;
            }

            Globals.LoggedInUser = user;
            MessageDialog md2 = new MessageDialog("Succesfull registration");
            await md2.ShowAsync();
        }
    }
}
