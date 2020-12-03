using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using TraveloreFE.Model;
using Windows.UI.Popups;
using Windows.UI.WindowManagement;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using HttpClient = Windows.Web.Http.HttpClient;

namespace TraveloreFE.ViewModel
{
    public class UserViewModel
    {
        public User User { get; set; }

        public UserViewModel()
        {
            User = new User();
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            string json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password
            });
            HttpClient httpClient = new HttpClient();
            try
            {
                var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Account"),
                new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                //MessageDialog md2 = new MessageDialog(res.Content.ToString());
                //await md2.ShowAsync();
                String access_token = res.Content.ToString();
                if (access_token == null)
                {
                    throw new Exception();
                }
                User user2 = new User();
                user2 = await GetUserDetails(access_token);
                MessageDialog md3 = new MessageDialog($"Email : {user2.Email}\n Firstname : {user2.FirstName}\n Lastname : {user2.LastName}");
                await md3.ShowAsync();
                return user2;
            } catch (Exception)
            {
                MessageDialog md = new MessageDialog("Error authenticating");
                await md.ShowAsync();
                return null;
            }
        }

        public async Task<User> GetUserDetails(String access_token)
        {
            string token = access_token;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer ", token);
                string response = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Customer"));
                MessageDialog md4 = new MessageDialog($"{response}");
                await md4.ShowAsync();
                User user3 = new User();
                user3 = JsonConvert.DeserializeObject<User>(response);
                user3.access_token = token;
                return user3;
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> RegisterUser(string email, string password, string confirmPassword, string firstname, string lastname)
        {
            string json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password,
                confirmPassword = confirmPassword,
                firstname = firstname,
                lastname = lastname
            });
            HttpClient httpClient = new HttpClient();
            try
            {
                var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Account"),
                new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                return JsonConvert.DeserializeObject<User>(res.Content.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
