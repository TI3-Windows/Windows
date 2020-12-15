using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using TraveloreFE.Model;
using Windows.Web.Http;
using HttpClient = Windows.Web.Http.HttpClient;

namespace TraveloreFE.ViewModel
{
    public class TravellistViewModel
    {
        //public User User { get; set; }
        public ObservableCollection<Travellist> Travellists { get; set; }
        public Travellist Travellist { get; set; }

        public TravellistViewModel(/*User us*/)
        {
            //User = us;
            Travellists = new ObservableCollection<Travellist>();
            loadTravellists();
        }

        private async void loadTravellists()
        {
            HttpClient httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(new Uri("http://localhost:5001/api/Travellist"));
            var travelLists = JsonConvert.DeserializeObject<IList<Travellist>>(json);
            //var travelLists = User.Travellists;
            foreach (var tl in travelLists)
            {
                Travellists.Add(tl);
            }
        }

        public async System.Threading.Tasks.Task AddNewTravellist(string name/*, string country, string street, string houseNr, DateTime? dateLeave, DateTime? dateBack*/)
        {
            //var travellist = new Travellist() { Name = name, Country = country, Street = street, HouseNr = houseNr, DateLeave = dateLeave, DateBack = dateBack };
            var test = new Travellist() { Name = name, Country = "country", Street = "street", HouseNr = "nr", DateLeave = DateTime.Now, DateBack = DateTime.Now };
            var travellistJson = JsonConvert.SerializeObject(test);

            HttpClient httpClient = new HttpClient();
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Travellist"),
                new HttpStringContent(travellistJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if(res.IsSuccessStatusCode)
            {
                Travellists.Add(JsonConvert.DeserializeObject<Travellist>(res.Content.ToString()));
            }
        }
    }
}
