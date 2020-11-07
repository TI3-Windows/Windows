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

        public List<Item> Items { get; set; }

        public Category()
        {
            Items = new List<Item>();
        }

        public Category(string name, bool done)
        {
            this.Name = name;
            this.DoneCat = done;
            Items = new List<Item>();
        }

        public void AddItem(Item i)
        {
            Items.Add(i);
        }
    }
}
