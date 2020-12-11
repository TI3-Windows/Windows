using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class Destination
    {
        private string _name;
        private string _street;
        private string _nr;
        private string _description;
        private string _city;
        private DateTime _visitTime;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Nr { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public DateTime VisitTime { get; set; }

        public Destination()
        {

        }

        public Destination(
            string name,
            string street,
            string nr,
            string description,
            string city,
            DateTime visitTime)
        {
            Name = name;
            Street = street;
            Nr = nr;
            Description = description;
            City = city;
            VisitTime = visitTime;
        }
    }
}
