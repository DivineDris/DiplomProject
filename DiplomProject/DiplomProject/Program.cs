using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomProject
{
    public class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Teacher
    {
        public string first_name, last_name, father_name;
        const int work_hours = 36;
    }
    public class Group
    {
        public string faculty, number;
        public List<Subject> subjects = new List<Subject>();
        const int work_hours = 36;
    }
    public class Subject
    {
        public string name;
        int amount_of_hours;
        public List<Teacher> teachers = new List<Teacher>();
    }

}
