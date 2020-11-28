using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TraveloreFE.Model;

namespace TraveloreFE.ViewModel
{
    class TravellistViewModel
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

    }
}
