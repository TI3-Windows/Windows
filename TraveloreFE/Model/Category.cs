using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
    public class Category
    {
        public int Id { get; set; }

        private string _name;
        public string Name { get; set; }

        private bool _doneCat;
        public bool DoneCat { get; set; }

        //public List<Item> Items { get; set; }
    }
}
