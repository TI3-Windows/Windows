using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
    public class Category : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }

        private string _name;
        public string Name {
            get 
            {
                return _name;
            }
            set 
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private bool _doneCat;
        public bool DoneCat { 
            get 
            {
                return _doneCat;
            } 
            set 
            {
                _doneCat = value;
                RaisePropertyChanged("DoneCat");
            } 
        }

        //public List<Item> Items { get; set; }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
