using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
    public class Item
    {
        public int Id { get; set; }

        private string _name;
        public string Name { get; set; }

        private int _amount;
        public string Amount { get; set; }

        private bool _doneItem;
        public bool DoneItem { get; set; }
    }
}
