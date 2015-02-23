using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingApp.Domain
{
    class Subject : IAssignment
    {
        public string Name { get; set; }
        public float Min { get; set; }
        public float Grade { get; set; }

        public int Year { get; set; }

        public ObservableCollection<Assignment> AllAssignments { get; set; }
        public ObservableCollection<Bonus> Bonuses { get; set; }
        public ObservableCollection<Demand> Demands { get; set; }

        public ObservableCollection<Assignment> Exams { get { return new ObservableCollection<Assignment>(AllAssignments.Where(x => x.AssignmentType == AssignmentType.Exam)); } }
        public ObservableCollection<Assignment> Practicals { get { return new ObservableCollection<Assignment>(AllAssignments.Where(x => x.AssignmentType == AssignmentType.Practical)); } }
        public ObservableCollection<Assignment> Exercises { get { return new ObservableCollection<Assignment>(AllAssignments.Where(x => x.AssignmentType == AssignmentType.Exercise)); } }

        private SubjectCalculator Calculator;

        public Subject()
        {
            Name = "";
            AllAssignments = new ObservableCollection<Assignment>();
            Bonuses = new ObservableCollection<Bonus>();
            Min = 5.5f;
            Demands = new ObservableCollection<Demand>();
            Calculator = new SubjectCalculator(this);
            Year = DateTime.Now.Year;
        }

        public Subject(string name)
        {
            Name = name;
            AllAssignments = new ObservableCollection<Assignment>();
            Bonuses = new ObservableCollection<Bonus>();
            Min = 5.5f;
            Demands = new ObservableCollection<Demand>();
            Calculator = new SubjectCalculator(this);
            Year = DateTime.Now.Year;
        }

        public Subject(string name, ObservableCollection<Assignment> exams)
        {
            Name = name;
            AllAssignments = exams;
            Bonuses = new ObservableCollection<Bonus>();
            Min = 5.5f;
            Demands = new ObservableCollection<Demand>();
            Calculator = new SubjectCalculator(this);
            Year = DateTime.Now.Year;
        }

        public Subject(string name, float min, ObservableCollection<Assignment> exams, ObservableCollection<Bonus> bonusses)
        {
            Name = name;
            AllAssignments = exams;
            Bonuses = bonusses;
            Min = min;
            Demands = new ObservableCollection<Demand>();
            Calculator = new SubjectCalculator(this);
            Year = DateTime.Now.Year;
        }


        public Subject(string name, float min, ObservableCollection<Assignment> exams, ObservableCollection<Demand> demands)
        {
            Name = name;
            AllAssignments = exams;
            Bonuses = new ObservableCollection<Bonus>();
            Demands = demands;
            Min = min;
            Calculator = new SubjectCalculator(this);
            Year = DateTime.Now.Year;
        }

        public Subject(string name, float min, ObservableCollection<Assignment> exams, ObservableCollection<Bonus> bonusses, ObservableCollection<Demand> demands)
        {
            Name = name;
            AllAssignments = exams;
            Bonuses = bonusses;
            Demands = demands;
            Min = min;
            Calculator = new SubjectCalculator(this);
            Year = DateTime.Now.Year;
        }

        public Subject(string name, float min, ObservableCollection<Assignment> exams, ObservableCollection<Demand> demands, int year)
        {
            Name = name;
            AllAssignments = exams;
            Bonuses = new ObservableCollection<Bonus>();
            Demands = demands;
            Min = min;
            Calculator = new SubjectCalculator(this);
            Year = year;
        }

        public float CalculateGrade()
        {
            return Calculator.CalculateGrade();
        }
    }
}
