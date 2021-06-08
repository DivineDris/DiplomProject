using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DiplomProject
{
    public partial class Form1 : Form
    {
        List<Teacher> teachers = new List<Teacher>();
        List<Subject> subjects = new List<Subject>();
        List<Group> groups = new List<Group>();
        TimeTable time_table = new TimeTable();
        Day[] days = new Day[6];
        string[] dep = { "В", "ВТ", "Д", "П", "Т", "Э" };
        int day = 1, hours = 0;
        ComboBox[] comboBoxes = new ComboBox[8];
        string[,] selected_items = new string[6, 8];
        public Form1()
        {
            InitializeComponent();
        }

        private void преподавательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeacherForm teacher_form = new TeacherForm();
            teacher_form.Show();
        }

        private void предметToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubjectForm subject_form = new SubjectForm();
            subject_form.Show();
        }

        private void группаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupForm group_form = new GroupForm();
            group_form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            comboBoxes[0] = comboBox3;
            comboBoxes[1] = comboBox4;
            comboBoxes[2] = comboBox5;
            comboBoxes[3] = comboBox6;
            comboBoxes[4] = comboBox7;
            comboBoxes[5] = comboBox8;
            comboBoxes[6] = comboBox9;
            comboBoxes[7] = comboBox10;
            foreach (ComboBox box in comboBoxes)
                box.Text = "Нет пары";
            label10.Text = "Часов осталось: " + time_table.Hours;
            ReadingFile("DataFiles\\Groups.bin");
            ReadingFile("DataFiles\\Teachers.bin");
            comboBox1.Items.AddRange(dep);
        }

        void ReadingFile(string str)
        {
            switch (str)
            {
                case "DataFiles\\Teachers.bin":
                    if (File.Exists(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str))
                    {
                        string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                        Stream openFileStream = File.OpenRead(FileName);
                        BinaryFormatter deserializer = new BinaryFormatter();
                        teachers = (List<Teacher>)deserializer.Deserialize(openFileStream);
                        openFileStream.Close();
                    }
                    break;
                case "DataFiles\\Subjects.bin":
                    if (File.Exists(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str))
                    {
                        string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                        Stream openFileStream = File.OpenRead(FileName);
                        BinaryFormatter deserializer = new BinaryFormatter();
                        subjects = (List<Subject>)deserializer.Deserialize(openFileStream);
                        openFileStream.Close();
                    }
                    break;
                case "DataFiles\\Groups.bin":
                    if (File.Exists(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str))
                    {
                        string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                        Stream openFileStream = File.OpenRead(FileName);
                        BinaryFormatter deserializer = new BinaryFormatter();
                        groups = (List<Group>)deserializer.Deserialize(openFileStream);
                        openFileStream.Close();
                    }
                    break;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            foreach(Group group in groups)
            {
                if(comboBox1.SelectedItem.ToString() == group.Dep)
                {
                    comboBox2.Items.Add(group.Number);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            foreach(Group group in groups)
            {
                if ((comboBox1.SelectedItem.ToString() == group.Dep) && (comboBox2.SelectedItem.ToString() == group.Number))
                {
                    time_table.CurrentGroup = group;
                    foreach (ComboBox comboBox in comboBoxes)
                    {
                        comboBox.Items.Add("Нет пары");
                        comboBox.Items.AddRange(FillSubjects(group));
                    }
                    foreach (GroupSubject subject in group.Subjects)
                    {
                        string str = subject.Name +" "+ subject.Hours + "ч.";
                        listBox1.Items.Add(str);
                    }    
                }

            }


        }

        string[] FillSubjects(Group group)
        {
            List<string> strs = new List<string>();
            string teachers_name = " - ";
            foreach (GroupSubject subject in group.Subjects)
            {
                foreach(Teacher teacher in subject.LeadingTeachers)
                    teachers_name = teachers_name + teacher.LastName + " " + teacher.FirstName + " " + teacher.FatherName + " / ";

                strs.Add(subject.Name + teachers_name);
                teachers_name = " - ";
            }

            return strs.ToArray();
        }


        void ChangeDay ()
        {
            if (day == 1)
                button2.Enabled = false;
            else
                button2.Enabled = true;

            if (day == 6)
                button3.Enabled = false;
            else
                button3.Enabled = true;
            label9.Text = DayNumber(day);

            }

        private void button3_Click(object sender, EventArgs e)
        {
            WritingDay();
            label10.Text = "Часов осталось: " + (time_table.Hours - HoursCounter(days[0]) - HoursCounter(days[1]) - HoursCounter(days[2]) - HoursCounter(days[3]) - HoursCounter(days[4]) - HoursCounter(days[5]));
            listBox1.Items.Clear();
            foreach (GroupSubject subject in time_table.CurrentGroup.Subjects)
                ChangeListBox(subject);
            if (day < 6)
                day++;
            ChangeDay();
            ReadingDay();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WritingDay();
            label10.Text = "Часов осталось: " + (time_table.Hours - HoursCounter(days[0]) - HoursCounter(days[1]) - HoursCounter(days[2]) - HoursCounter(days[3]) - HoursCounter(days[4]) - HoursCounter(days[5]));
            listBox1.Items.Clear();
            foreach (GroupSubject subject in time_table.CurrentGroup.Subjects)
                ChangeListBox(subject);
            if (day > 0)
                day--;
            ChangeDay();
            ReadingDay();
        }

        void WritingDay()
        {
            days[day - 1] = new Day();
            days[day - 1].Name = DayNumber(day);
            int counter = 0;
            foreach (ComboBox box in comboBoxes)
            {

                    if ((box.SelectedItem == null) || (box.SelectedItem == "Нет пары"))
                    {
                        days[day - 1].DaySubjects[counter] = new GroupSubject();
                        days[day - 1].DaySubjects[counter].Name = "Нет пары";
                        days[day - 1].DaySubjects[counter].LeadingTeachers = null;
                        days[day - 1].DaySubjects[counter].Hours = 0;
                    }
                    else
                    {
                        foreach (GroupSubject subject in time_table.CurrentGroup.Subjects)
                        {
                            string[] subject_name = box.SelectedItem.ToString().Split('-');
                            if (AntiSpace(subject_name[0]) == subject.Name.ToString())
                            {
                                days[day - 1].DaySubjects[counter] = new GroupSubject();
                                days[day - 1].DaySubjects[counter].Name = subject.Name.ToString();
                                days[day - 1].DaySubjects[counter].LeadingTeachers.AddRange(subject.LeadingTeachers);
                                days[day - 1].DaySubjects[counter].Hours = 2;
                                break;
                            }

                        }

                    }
                
               
                ++counter;
                
            }
            foreach (ComboBox box in comboBoxes)
            {
                box.SelectedItem = null;
                box.Text = "Нет пары";
            }


        }

        int HoursCounter(Day day)
        {
            int hours_per_day = 0;
            if (day != null)
            {
                foreach (GroupSubject subject in day.DaySubjects)
                    hours_per_day += subject.Hours;
            }
            return hours_per_day;

        }

        int SubjectHoursCounter(GroupSubject subject ,Day day)
        {
            int hours_per_subject = 0;
            if (day != null)
            {
                foreach(GroupSubject all_subjects in day.DaySubjects)
                {
                        if(subject.Name == all_subjects.Name)
                            hours_per_subject += all_subjects.Hours;
                }
            }
            return hours_per_subject;
        }
        void ChangeListBox(GroupSubject subject)
        {
                string str = subject.Name + " " + (subject.Hours - SubjectHoursCounter(subject,days[0]) - SubjectHoursCounter(subject,days[1]) - SubjectHoursCounter(subject,days[2]) - SubjectHoursCounter(subject,days[3]) - SubjectHoursCounter(subject,days[4]) - SubjectHoursCounter(subject,days[4])) + "ч.";
                listBox1.Items.Add(str);
        }

        void ReadingDay()
        {
            if(days[day-1] != null)
            {
                for(int i = 0; i < 8; i++)
                {

                    if (days[day - 1].DaySubjects[i].LeadingTeachers != null)
                    {
                        string teachers_name = " - ";
                        foreach (Teacher teacher in days[day - 1].DaySubjects[i].LeadingTeachers)
                            teachers_name = teachers_name + teacher.LastName + " " + teacher.FirstName + " " + teacher.FatherName + " / ";
                        comboBoxes[i].Text = days[day - 1].DaySubjects[i].Name + teachers_name;
                    }
                    else
                        comboBoxes[i].Text = days[day - 1].DaySubjects[i].Name;

                }
                    
            }
        }

        string DayNumber(int day)
        {
            string str = null;
            switch (day)
            {
                case 1:
                    str = "Понедельник";
                    break;

                case 2:
                    str = "Вторник";
                    break;

                case 3:
                    str = "Среда";
                    break;

                case 4:
                    str = "Четверг";
                    break;

                case 5:
                    str = "Пятница";
                    break;

                case 6:
                    str = "Суббота";
                    break;
            }
            return str;

        }

        void TeacherBusyInThisTime(ComboBox comboBox)
        {
            string[] str = comboBox.SelectedItem.ToString().Split('-');
            string[] teachers_names = str[1].Split('/');
            foreach (string s in teachers_names)
            {
                s.Trim(' ');
                foreach (Teacher teacher in teachers)
                {
                    string[] LLF = s.Split(' ');
                    if ((LLF[0] == teacher.LastName) && (LLF[1] == teacher.FirstName) && (LLF[2] == teacher.FatherName))
                    {
                        int nummber_of_lesson = new int();
                        foreach (char x in comboBox.Name)
                            if (Char.IsDigit(x))
                                nummber_of_lesson = Convert.ToInt32(x);
                        teacher.WhenBusy[day, nummber_of_lesson] = true;
                    }

                }
            }
        }


        private void label9_TextChanged(object sender, EventArgs e)
        {
            
        }



        string AntiSpace(string str)
        {
            char[] cs = new char[str.Length];
            string result = string.Empty;
            int index = 0;
            foreach (char c in str)
            {
                cs[index] = c;
                index++;
            }
            for(int i = 0; i < cs.Length-1; i++)
                result = result + cs[i];
            return result;

        }
    }
}
