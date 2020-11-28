using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TraveloreFE.Model;
using Windows.UI.WindowManagement;

namespace TraveloreFE.ViewModel
{
    class UserViewModel
    {
        public User User { get; set; }

        public UserViewModel()
        {
            User = new User();
            loadUser();
        }

        private async void loadUser()
        {
            //HttpClient httpClient = new HttpClient();
            //var json = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Customer"));
            //User = JsonConvert.DeserializeObject<User>(json);
        }
    }
}
