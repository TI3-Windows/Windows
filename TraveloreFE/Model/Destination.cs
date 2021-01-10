using System;

namespace TraveloreFE.Model
{
    public class Destination
    {
        private string _name;
        private string _street;
        private string _nr;
        private string _description;
        private string _city;
        private DateTime _visitTime;

        
        public int Id { get; set; }
        //public string Name { get; set; }
        //public string Street { get; set; }
        //public string Nr { get; set; }
        //public string Description { get; set; }
        //public string City { get; set; }
        //public DateTime VisitTime { get; set; }


        public string Name
        {
            get { return _name; }
            set
            { 
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty!");
                }
                _name = value; 
            }
        }

        public string Street
        {
            get { return _street; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Street cannot be empty!");
                }
                _street = value; 
            }
        }

        public string Nr
        {
            get { return _nr; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nr cannot be empty!");
                }

                _nr = value; 
            }
        }

        public string Description
        {
            get { return _description; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be empty!");
                }
                _description = value; 
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("City cannot be empty!");
                }
                _city = value; 
            }
        }

        public DateTime VisitTime
        {
            get { return _visitTime; }
            set { _visitTime = value; }
        }

    }
}