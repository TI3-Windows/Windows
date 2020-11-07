using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class Item
    {
        private string _name;
        private int _amount;
        private bool _doneItem;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool DoneItem { get; set; }

        public Item()
        {

        }

        public Item(string name, int amount, bool done)
        {
            this.Name = name;
            this.Amount = amount;
            this.DoneItem = done;
        }
    }
}
