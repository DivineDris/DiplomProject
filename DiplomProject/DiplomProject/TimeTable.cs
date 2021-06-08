using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    [Serializable()]
    public class TimeTable
    {
        Group current_group;
        int hours = 36;
        Day[] days = new Day[6];

        public Group CurrentGroup
        {
            get { return current_group; }
            set { current_group = value; }
        }

        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }

        public Day[] Days
        {
            get { return days; }
            set { days = value; }
        }
    }
}
