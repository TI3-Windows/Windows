using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class TravelList
    {
        private string _name;
        private string _country;
        private string _street;
        private int _houseNr;
        private DateTime _dateLeave;
        private DateTime _dateBack;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public DateTime DateLeave { get; set; }
        public DateTime DateBack { get; set; }

        public List<Category> Categories { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Destination> Itinerary {get; set;}

        public TravelList()
        {

        }

        public TravelList(string name, string country, string street, string houseNr, DateTime dateLeave, DateTime dateBack)
        {
            this.Name = name;
            this.Country = country;
            this.Street = street;
            this.HouseNr = houseNr;
            this.DateLeave = dateLeave;
            this.DateBack = dateBack;
            Categories = new List<Category>();
            Tasks = new List<Task>();
            Itinerary = new List<Destination>();
        }

        public void AddCategory(Category c)
        {
            Categories.Add(c);
        }

        public void AddDestination(Destination des)
        {
            Itinerary.Add(des);
        }

        public void AddTask(Task t)
        {
            Tasks.Add(t);
        }
    }
}
