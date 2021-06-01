using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    [Serializable()]
    public class Group
    {
        string dep, number;
        int year;
        List<GroupSubject> subjects = new List<GroupSubject>();

        public string Dep
        {
            get { return dep; }
            set { dep = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public List<GroupSubject> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }
    }
}
