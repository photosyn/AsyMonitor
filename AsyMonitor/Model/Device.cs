using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyMonitor
{
    class Device
    {
        bool ischeck;
        int did;
        string name;
        string location;


        public bool Ischeck { get => !ischeck; set => ischeck = !value; }
        public int Did { get => did; set => did = value; }
        public string Name { get => name; set => name = value; }
        public string Location { get => location; set => location = value; }
    }
}
