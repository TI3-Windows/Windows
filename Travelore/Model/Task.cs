using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class Task
    {
        private string _name;
        private string _description;
        private bool _doneTask;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool DoneTask { get; set; }
    }
}
