using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelore.Model;

namespace Travelore.DTOs
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<TravelList> TravelLists { get; set; }

        public CustomerDTO() { }

        public CustomerDTO(Customer customer) : this()
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
            TravelLists = new List<TravelList>();
            foreach (TravelList item in customer.TravelLists)
            {
                TravelLists.Add(new TravelList() { 
                    Id = item.Id,
                    Name = item.Name,
                    Country = item.Country,
                    Street = item.Street,
                    HouseNr = item.HouseNr,
                    DateLeave = item.DateLeave,
                    DateBack = item.DateBack,
                    Categories = item.Categories,
                    Tasks = item.Tasks,
                    Itinerary = item.Itinerary
                });
            }
        }
    }
}
