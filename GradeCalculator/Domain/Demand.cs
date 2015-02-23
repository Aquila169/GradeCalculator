using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingApp.Domain
{
    class Demand
    {
        public string Name { get; set; }
        public float Max { get; set; }

        public bool Met { get; set; }

        public Demand(string name, float max)
        {
            Name = name;
            Max = max;
        }
    }
}
