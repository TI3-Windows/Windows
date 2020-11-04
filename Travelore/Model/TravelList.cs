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
        public int HouseNr { get; set; }
        public DateTime DateLeave { get; set; }
        public DateTime DateBack { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}
