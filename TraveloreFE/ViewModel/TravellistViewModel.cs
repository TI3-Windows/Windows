using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using TraveloreFE.Model;
using TraveloreFE.ViewModel.Commands;
using Windows.Web.Http;
using HttpClient = Windows.Web.Http.HttpClient;

namespace TraveloreFE.ViewModel
{
    public class TravellistViewModel
    {
        //public User User { get; set; }
        public ObservableCollection<Travellist> Travellists { get; set; }
        public Travellist Travellist { get; set; }

        public ICommand DeleteTravellistCommand { get; set; }

        public TravellistViewModel(/*User us*/)
        {
            //User = us;
            Travellists = new ObservableCollection<Travellist>();
            DeleteTravellistCommand = new DeleteTravellistCommand(this);
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

        public async System.Threading.Tasks.Task AddNewTravellist(Travellist tl/*, string country, string street, string houseNr, DateTime? dateLeave, DateTime? dateBack*/)
        {
            //var travellist = new Travellist() { Name = name, Country = country, Street = street, HouseNr = houseNr, DateLeave = dateLeave, DateBack = dateBack };
            var test = new Travellist() { Name = tl.Name, Country = tl.Country, Street = "street", HouseNr = "nr", DateLeave = tl.DateLeave, DateBack = tl.DateBack };
            var travellistJson = JsonConvert.SerializeObject(test);

            HttpClient httpClient = new HttpClient();
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Travellist"),
                new HttpStringContent(travellistJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if(res.IsSuccessStatusCode)
            {
                Travellists.Add(JsonConvert.DeserializeObject<Travellist>(res.Content.ToString()));
            }
        }

        // Delete A Travellist
        public async System.Threading.Tasks.Task DeleteSelectedTravellist(int id)
        {
            var taskIdJson = JsonConvert.SerializeObject(id);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Travellist/{id}";
            var res = await httpClient.DeleteAsync(new Uri(url));
            if (res.IsSuccessStatusCode)
            {
                var deletedTravellist = Travellists.SingleOrDefault((t) => t.Id == id);
                if (deletedTravellist != null)
                {
                    Travellists.Remove(deletedTravellist);
                }
            }
        }
    }
}
