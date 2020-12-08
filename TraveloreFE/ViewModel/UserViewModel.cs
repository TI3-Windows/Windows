using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<String> AuthenticateUser(string email, string password)
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
                String access_token = res.Content.ToString();
                if (access_token == null)
                {
                    throw new Exception();
                }
                return access_token;
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
                httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", token);
                var response = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Customer"));
                User = JsonConvert.DeserializeObject<User>(response);
                User.access_token = token;
                return User;
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<String> RegisterUser(string email, string password, string confirmPassword, string firstname, string lastname)
        {
            string json = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password,
                firstname = firstname,
                lastname = lastname,
                passwordConfirmation = confirmPassword
            });
            HttpClient httpClient = new HttpClient();
            try
            {
                var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Account"),
                new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
                String access_token = res.Content.ToString();
                if(access_token == null)
                {
                    throw new Exception();
                }
                return access_token;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
