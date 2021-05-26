using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    [Serializable()]
    public class Subject
    {
        string name;
        List<Teacher> leading_teachers = new List<Teacher>();

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Teacher> LeadingTeachers
        {
            get { return leading_teachers; }
            set { leading_teachers = value; }
        }
    }
}
