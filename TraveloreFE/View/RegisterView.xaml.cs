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

            if (tbEmail.Text.Length == 0)
            {
                emailStatusText.Text = "Email can't be empty";
            }
            else
            {
                tbEmail.Text = String.Empty;
                if (tbFirstname.Text.Length == 0)
                {
                    firstnameStatusText.Text = "Firstname can't be empty";
                }
                else
                {
                    tbFirstname.Text = String.Empty;
                    if (tbLastname.Text.Length == 0)
                    {
                        lastnameStatusText.Text = "Lastname can't be empty";
                    }
                    else
                    {
                        tbLastname.Text = String.Empty;
                        if (tbPassword.Password.Length == 0)
                        {
                            passwordStatusText.Text = "Password can't be empty";
                        } 
                        else
                        {
                            tbPassword.Password = String.Empty;
                            /*if (!tbPassword.Password.Contains(tbConfirmPassword.Password))
                            {
                                confirmPasswordStatusText.Text = "Passwords must match";
                            }
                            else
                            {*/
                                tbConfirmPassword.Password = String.Empty;
                                try
                                {
                                    String token = await uvm.RegisterUser(email, password, confirmPassword, firstname, lastname);
                                    if (token == null)
                                        throw new Exception();
                                    User user = await uvm.GetUserDetails(token);
                                    if (user != null)
                                    {
                                        Globals.LoggedInUser = user;
                                        Frame rootFrame = Window.Current.Content as Frame;
                                        rootFrame.Navigate(typeof(TravellistsView));
                                    } 
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                catch(Exception)
                                {
                                    apiCallStatusText.Text = "Error while trying to register. Try again later ...";
                                }
                            //}
                        }
                    }
                }
               
            }
            

            
        }
    }
}
