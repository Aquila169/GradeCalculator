using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingApp.Domain
{
    class Bonus
    {
        public string Name { get; set; }
        public bool HasStrictVal { get { return this.StrictVal >= 0f; } }
        public float StrictVal { get; set; }
        public IAssignment OnTopOf { get; set; }

        public bool Scored { get; set; }
        public float ScoredVal { get; set; }

        public Bonus(string name)
        {
            Name = name;
            StrictVal = -1;
        }

        public Bonus(string name, float strictval)
        {
            Name = name;
            StrictVal = strictval;
        }
    }
}
