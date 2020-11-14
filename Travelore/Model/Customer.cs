using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class Customer
    {
        //add extra properties if needed
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<TravelList> TravelLists { get; private set; }

        public Customer() {
            TravelLists = new List<TravelList>();
        }

        public void AddTravelLists(TravelList l)
        {
            TravelLists.Add(l);
        }

        public void DeleteTravelList(TravelList l)
        {
            TravelLists.Remove(l);
        }
    }
}
