using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingApp.Domain
{
    interface IAssignment
    {
        string Name { get; set; }
        float Grade { get; set; }
    }
}
