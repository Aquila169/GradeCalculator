using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingApp.Domain
{
    public enum AssignmentType
    {
        Exam,
        Practical,
        Exercise
    };

    class Assignment : IAssignment
    {
        public string Name { get; set; }
        public float Weight { get; set; }
        public AssignmentType AssignmentType { get; set; }

        public float Grade { get; set; }

        public Assignment(string name, float weight, AssignmentType type)
        {
            Name = name;
            Weight = weight;
            AssignmentType = type;
        }
    }
}
