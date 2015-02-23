using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingApp.Domain
{
    class SubjectCalculator
    {
        public Subject Subject { get; set; }
        public SubjectCalculator(Subject subject)
        {
            Subject = subject;
        }

        public float CalculateGrade()
        {
            Subject.Grade = 0;

            float maxGrade = 10;
            //First we check if all demands are met.
            //We need to keep the maximum we can get for each demand.
            //So if a demand is not met, check whether the maxGradeAfterDemands < or not.
            foreach (Demand demand in Subject.Demands)
                if (!demand.Met)
                    //If a demand is not met, check whether it gives us a lower grade.
                    if (maxGrade > demand.Max)
                        maxGrade = demand.Max;

            //Figure out which bonuses have been scored, and add the appropriate amount of grade to it.
            Dictionary<string, float> bonusDictionary = new Dictionary<string, float>();
            foreach(Bonus bonus in Subject.Bonuses)
                if (bonus.Scored)
                {
                    if (bonus.OnTopOf.Name == Subject.Name)
                    {
                        Subject.Grade += bonus.HasStrictVal ? bonus.StrictVal : bonus.ScoredVal;
                        continue;
                    }

                    if(bonusDictionary.ContainsKey(bonus.OnTopOf.Name))
                        bonusDictionary[bonus.OnTopOf.Name] += bonus.HasStrictVal ? bonus.StrictVal : bonus.ScoredVal;
                    else
                        bonusDictionary.Add(bonus.OnTopOf.Name, bonus.HasStrictVal ? bonus.StrictVal : bonus.ScoredVal);
                }

            foreach (Assignment assignment in Subject.AllAssignments)
                if(bonusDictionary.ContainsKey(assignment.Name))
                    Subject.Grade += (assignment.Grade + bonusDictionary[assignment.Name])*assignment.Weight;
                else
                    Subject.Grade += assignment.Grade * assignment.Weight;

            if (maxGrade < Subject.Grade)
                Subject.Grade = maxGrade;

            return Subject.Grade;
        }
    }
}
