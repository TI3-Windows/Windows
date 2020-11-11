using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
    public class Task
    {
        private string _name;
        private string _description;
        private DateTime _endDate;
        private bool _doneTask;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public bool DoneTask { get; set; }
    }
}
