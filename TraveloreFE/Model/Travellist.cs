using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveloreFE.Model
{
   public class Travellist : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private string _country;
        private string _street;
        private string _houseNr;
        private DateTime? _dateLeave;
        private DateTime? _dateBack;

        public int Id { get; set; }
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
        public string Country {
            get {
                return _country;
            }
            set {
                _country = value;
                RaisePropertyChanged("Country");
            }
        }
        public string Street {
            get {
                return _street;
            } 
            set {
                _street = value;
                RaisePropertyChanged("Street");
            } 
        }
        public string HouseNr {
            get {
                return _houseNr;    
            }
            set {
                _houseNr = value;
                RaisePropertyChanged("HouseNr");
            } 
        }
        public DateTime? DateLeave {
            get {
                return _dateLeave;    
            }
            set {
                _dateLeave = value;
                RaisePropertyChanged("DateLeave");
            } 
        }
        public DateTime? DateBack {
            get {
                return _dateBack;    
            }
            set {
                _dateBack = value;
                RaisePropertyChanged("DateBack");
            } 
        }

        public List<Category> Categories { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Destination> Itinerary { get; set; }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void AddDestination(Destination destination)
        {
            Itinerary.Add(destination);
        }
    }
}
