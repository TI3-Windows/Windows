using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
    public class User
    {
        public string _firstName;
        public string _lastName;
        public string _email;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string access_token { get; set; }
        public List<Travellist> Travellists { get; set; }
    }
}
