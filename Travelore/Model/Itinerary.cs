using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class Itinerary
    {
        public int Id { get; set; }
        public TravelList TravelList { get; set; }
        public List<Destination> Destinations { get; set; }

        public Itinerary()
        {
            Destinations = new List<Destination>();
        }

        public void AddDestination(Destination des) 
        {
            Destinations.Add(des);
        }
    }
}
