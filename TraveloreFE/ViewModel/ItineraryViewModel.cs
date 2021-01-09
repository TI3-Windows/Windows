using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TraveloreFE.Model;
using TraveloreFE.ViewModel.Commands;
using Windows.ApplicationModel.Store.LicenseManagement;
using Windows.Web.Http;

namespace TraveloreFE.ViewModel
{
    public class ItineraryViewModel
    {
        public Travellist Travellist { get; set; }
        public List<Destination> Itinerary { get; set; }

        public ICommand DeleteDestinationCommand  { get; set; }

        public ItineraryViewModel(Travellist tl)
        {
            Travellist = tl;
            DeleteDestinationCommand = new DeleteDestinationCommand(this);
            //Array.Sort<Destination>(tl.Itinerary.ToArray(), new Comparison<Destination>(
            //    (i1, i2) => i2.VisitTime.CompareTo(i1.VisitTime)));

            tl.Itinerary.Sort(new Comparison<Destination>(
                (i1, i2) => i1.VisitTime.CompareTo(i2.VisitTime)));
            Itinerary = tl.Itinerary;
        }

        public async System.Threading.Tasks.Task AddNewDestination(Destination destination)
        {
            var travellistJson = JsonConvert.SerializeObject(destination);

            HttpClient httpClient = new HttpClient();
            var res = await httpClient.PostAsync(new Uri("http://localhost:5001/api/Travellist/Destination/"+Travellist.Id),
                new HttpStringContent(travellistJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                Travellist.AddDestination(JsonConvert.DeserializeObject<Destination>(res.Content.ToString()));
            }
        }

        // Delete A Destination
        public async System.Threading.Tasks.Task DeleteSelectedDestination(int id)
        {
            var tlid = Travellist.Id;
            var taskIdJson = JsonConvert.SerializeObject(id);
            HttpClient httpClient = new HttpClient();
            var url = $"http://localhost:5001/api/Travellist/Destination/{tlid}/{id}";
            var res = await httpClient.DeleteAsync(new Uri(url));
            if (res.IsSuccessStatusCode)
            {
                var deletedDestination = Itinerary.SingleOrDefault((t) => t.Id == id);
                if (deletedDestination != null)
                {
                    Itinerary.Remove(deletedDestination);
                }
            }
        }

    }
}
