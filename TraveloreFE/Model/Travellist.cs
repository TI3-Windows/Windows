using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
   public class Travellist
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
        public List<Destination> Itinerary { get; set; }
    }
}
