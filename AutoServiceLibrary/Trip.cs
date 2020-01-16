using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceLibrary
{
    public class Trip
    {
        public int IdTrip { get; set; }
        public string Name { get; set; }
        public bool ActiveFlag { get; set; }
        public Trip() { }
        public Trip(string name, bool activeFlag = true)
        {
            Name = name;
            ActiveFlag = activeFlag;
        }
        public Trip(int idTrip, string name, bool activeFlag) : this (name, activeFlag)
        {
            IdTrip = idTrip;
        }

        public void Delete()
        {
            ActiveFlag = false;
        }
        
    }
}
