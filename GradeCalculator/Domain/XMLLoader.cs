using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GradingApp.Domain
{
    static class XMLParser
    {
        public static Subject ParseSubject(string filePath)
        {
            string innerText = "";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    innerText = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("The file could not be read");
                return null;
            }

            XmlReader xmlReader = XmlReader.Create(new StringReader(innerText));

            List<KeyValuePair<string, Bonus>> parsedBoni = new List<KeyValuePair<string, Bonus>>();//<Bonus>();
            List<Assignment> parsedExams = new List<Assignment>();
            List<Demand> parsedDemands = new List<Demand>();

            string vaknaam = "";
            float min = 0;
            int year = DateTime.Now.Year;

            while (xmlReader.Read())
            {
                string tagname = xmlReader.Name;
                if(xmlReader.NodeType == XmlNodeType.Element)
                    switch (tagname)
                    {
                        case "Subject":
                        {
                            vaknaam = xmlReader.GetAttribute("Name");
                            min = float.Parse(xmlReader.GetAttribute("Min"), CultureInfo.InvariantCulture);
                            year = xmlReader.GetAttribute("Year") != null ? int.Parse(xmlReader.GetAttribute("Year"), CultureInfo.InvariantCulture) : (DateTime.Now.Month < 7 ? DateTime.Now.Year-1 : DateTime.Now.Year);
                            break;
                        }
                        case "Exam":
                        {
                            string naam = xmlReader.GetAttribute("Name");
                            float weight = float.Parse(xmlReader.GetAttribute("Weight"), CultureInfo.InvariantCulture);
                            parsedExams.Add(new Assignment(naam, weight, AssignmentType.Exam));
                            break;
                        }
                        case "Exercise":
                        {
                            string name = xmlReader.GetAttribute("Name");
                            float weight = float.Parse(xmlReader.GetAttribute("Weight"), CultureInfo.InvariantCulture);
                            parsedExams.Add(new Assignment(name, weight, AssignmentType.Exercise));
                            break;
                        }
                        case "Practical":
                        {
                            string name = xmlReader.GetAttribute("Name");
                            float weight = float.Parse(xmlReader.GetAttribute("Weight"), CultureInfo.InvariantCulture);
                            parsedExams.Add(new Assignment(name, weight, AssignmentType.Practical));
                            break;
                        }
                        case "Demand":
                        {
                            string nameee = xmlReader.GetAttribute("Name");
                            float max = float.Parse(xmlReader.GetAttribute("Max"), CultureInfo.InvariantCulture);
                            parsedDemands.Add(new Demand(nameee, max));
                            break;
                        }
                        case "Bonus":
                        {
                            string name = xmlReader.GetAttribute("Name");
                            string onTopOf = xmlReader.GetAttribute("For");
                            float strictval = -1f;
                            if (xmlReader.GetAttribute("StrictValue") != null)
                                strictval = float.Parse(xmlReader.GetAttribute("StrictValue"), CultureInfo.InvariantCulture);

                            parsedBoni.Add(new KeyValuePair<string, Bonus>(onTopOf, strictval >= 0f ? new Bonus(name, strictval) : new Bonus(name)));
                            break;
                        }
                    }
            }

            List<IAssignment> all = parsedExams.Cast<IAssignment>().ToList();
            //all.AddRange(parsedDemands);

            Subject subject = new Subject(vaknaam, min, new ObservableCollection<Assignment>(parsedExams), new ObservableCollection<Demand>(parsedDemands), year);

            //Now match up all the boni with their assignments
            foreach (KeyValuePair<string, Bonus> bonus in parsedBoni)
            {
                string onTopOf = bonus.Key;
                foreach(IAssignment assignemnt in all)
                    if (assignemnt.Name.Equals(onTopOf))
                        bonus.Value.OnTopOf = assignemnt;
                    else if (onTopOf.Equals("Subject"))
                        bonus.Value.OnTopOf = subject;
            }

            subject.Bonuses = new ObservableCollection<Bonus>(parsedBoni.Select(x => x.Value));

            return subject;
        }
    }
}
