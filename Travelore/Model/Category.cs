using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class Category
    {
        private string _name;
        private bool _doneCat;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool DoneCat { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
