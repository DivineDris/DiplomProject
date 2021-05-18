using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomProject
{
    public class AllData
    {
        List<Teacher> all_teachers = new List<Teacher>();



        public void AddTeacherToList(Teacher teacher)
        {
            all_teachers.Add(teacher);
        }
    }
}
