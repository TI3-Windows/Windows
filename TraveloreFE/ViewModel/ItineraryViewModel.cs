using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraveloreFE.Model;

namespace TraveloreFE.ViewModel
{
    class ItineraryViewModel
    {
        public Travellist Travellist { get; set; }
        public List<Destination> Itinerary { get; set; }

        public ItineraryViewModel(Travellist tl)
        {
            Travellist = tl;
            Itinerary = tl.Itinerary;
            Console.WriteLine(tl);
        }
    }
}
