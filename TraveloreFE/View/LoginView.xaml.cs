using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class LoginView : Page
    {
        Frame mainContent;

        public LoginView()
        {
            this.InitializeComponent();
        }

        private async void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            if(usernamebox.Text.Length == 0)
            {
                usernameStatusText.Text = "Please enter a username";
            }
            else
            {
                usernameStatusText.Text = string.Empty;
                if(passwordBox.Password.Length == 0)
                {
                    passwordStatusText.Text = "Please enter a password";
                }
                else
                {
                    passwordStatusText.Text = string.Empty;
                    MessageDialog md = new MessageDialog($"Username : {usernamebox.Text} \n Password : {passwordBox.Password}");
                    await md.ShowAsync();
                }
            }

            
        }

        private void registerViewBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(RegisterView));
        }

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(TravellistsView));
        }
    }
}
