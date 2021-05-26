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
        int year, hours;
        List<Subject> subjects = new List<Subject>();
        List<Teacher> teachers = new List<Teacher>();

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
        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public List<Subject> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        public List<Teacher> Teachers
        {
            get { return teachers; }
            set { teachers = value; }
        }
    }
}
