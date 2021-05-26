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
        Teacher leading_teacher = new Teacher();

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Teacher LeadingTeacher
        {
            get { return leading_teacher; }
            set { leading_teacher = value; }
        }
    }
}
