using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
            //Array.Sort<Destination>(tl.Itinerary.ToArray(), new Comparison<Destination>(
            //    (i1, i2) => i2.VisitTime.CompareTo(i1.VisitTime)));

            tl.Itinerary.Sort(new Comparison<Destination>(
                (i1, i2) => i1.VisitTime.CompareTo(i2.VisitTime)));
            Itinerary = tl.Itinerary;
            Console.WriteLine(tl);
        }
    }
}
