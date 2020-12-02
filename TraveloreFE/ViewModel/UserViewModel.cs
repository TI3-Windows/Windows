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

        /*public async System.Threading.Tasks.Task login()
        {
            var json = new User() { Email = "customer1@hogent.be", Password = "P@ssword1111" };
            var userJson = JsonConvert.SerializeObject(json);
            HttpClient httpClient = new HttpClient();
            //var json = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Customer"));
            //User = JsonConvert.DeserializeObject<User>(json);
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Account"),
                new HttpStringContent(userJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                //User = JsonConvert.DeserializeObject<User>(res.Content.ToString());
                MessageDialog md = new MessageDialog("Succes");
                await md.ShowAsync();
               *//* MessageDialog md2 = new MessageDialog(
                    $"Email : {User.Email}\n" +
                    $"Firstname : {User.FirstName}\n" +
                    $"Lastname : {User.LastName}\n" +
                    $"Travellists : {User.Travellists}\n");
                await md2.ShowAsync();*//*
            }
            else {
                MessageDialog md = new MessageDialog("Failure");
                await md.ShowAsync();
            }
        }*/

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
                return JsonConvert.DeserializeObject<User>(res.Content.ToString());
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> GetUserDetails(User user)
        {
            string access_token = user.access_token;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", access_token);
                string response = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Customer"));
                user = JsonConvert.DeserializeObject<User>(response);
                user.access_token = access_token;
                return user;
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
