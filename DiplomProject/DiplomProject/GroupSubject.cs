using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    [Serializable()]
    public class GroupSubject : Subject
    {
        int hours;
        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }
    }
}
