using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travelore.Model
{
    public class Task
    {
        private string _description;
        private bool _doneTask;
        private DateTime _endDate;

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? EndDate { get; set; }
        public bool DoneTask { get; set; }

        public Task()
        {

        }

        public Task(string description, DateTime? endDate, bool done)
        {
            this.Description = description;
            this.EndDate = endDate;
            this.DoneTask = done;
        }
    }
}
