using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
    public class Task : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string _description;
        private bool _doneTask;

        public int Id { get; set; }
        public string Description {
            get {
                return _description;
            }
            set {
                _description = value;
                RaisePropertyChanged("Description");
            } 
        }
        public bool DoneTask {
            get {
                return _doneTask;
            }
            set {
                _doneTask = value;
                RaisePropertyChanged("DoneTask");
            } 
        }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
