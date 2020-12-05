using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
    public class Item
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }

        private string _name;
        public string Name
        {
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

        private int _amount;
        public int Amount 
        { 
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                RaisePropertyChanged("Amount");
            }
        }

        private bool _doneItem;
        public bool DoneItem
        {
            get
            {
                return _doneItem;
            }
            set
            {
                _doneItem = value;
                RaisePropertyChanged("DoneItem");
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
