using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    [Serializable()]
    public class Day
    {
        string name;
        GroupSubject[] day_subjects = new GroupSubject[8];


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public GroupSubject[] DaySubjects
        {
            get { return day_subjects; }
            set { day_subjects = value; }
        }

    }
}
