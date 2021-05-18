using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomProject
{
    static class Program
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

    class Teacher
    {
        string first_name, last_name;
        List<Subject> subjects = new List<Subject>();
        const int work_hours = 36;
    }
    class Group
    {
        string faculty, number;
        List<Subject> subjects = new List<Subject>();
        const int work_hours = 36;
    }
    class Subject
    {
        string name;
        int amount_of_hours;
        List<Teacher> teachers = new List<Teacher>();
    }

}
