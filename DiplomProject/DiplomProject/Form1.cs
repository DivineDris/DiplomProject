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
        int day = 1;
        ComboBox[] comboBoxes = new ComboBox[8];
        ComboBox[] teachers_comboBoxes = new ComboBox[8];
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
            teachers_comboBoxes[0] = comboBox11;
            teachers_comboBoxes[1] = comboBox12;
            teachers_comboBoxes[2] = comboBox13;
            teachers_comboBoxes[3] = comboBox14;
            teachers_comboBoxes[4] = comboBox15;
            teachers_comboBoxes[5] = comboBox16;
            teachers_comboBoxes[6] = comboBox17;
            teachers_comboBoxes[7] = comboBox18;
            ReadingFile("DataFiles\\Groups.bin");
            comboBox1.Items.AddRange(dep);
        }

        void ReadingFile(string str)
        {
            switch (str)
            {
                case "DataFiles\\Teachers.bin":
                    string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                    Stream openFileStream = File.OpenRead(FileName);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    teachers = (List<Teacher>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    break;
                case "DataFiles\\Subjects.bin":
                    FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                    openFileStream = File.OpenRead(FileName);
                    deserializer = new BinaryFormatter();
                    subjects = (List<Subject>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    break;
                case "DataFiles\\Groups.bin":
                    FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                    openFileStream = File.OpenRead(FileName);
                    deserializer = new BinaryFormatter();
                    groups = (List<Group>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
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
                        comboBox.Items.AddRange(FillSubjects(group));
                   // foreach (ComboBox comboBox in teachers_comboBoxes)
                    //{

                    //}
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
            foreach (GroupSubject subject in group.Subjects)
                strs.Add(subject.Name);
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
            if (day < 6)
                day++;
            ChangeDay();
            ReadingDay();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WritingDay();
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

                if (box.SelectedItem == null)
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
                        if (box.SelectedItem.ToString() == subject.Name.ToString())
                        {
                            days[day - 1].DaySubjects[counter] = new GroupSubject();
                            days[day - 1].DaySubjects[counter].Name = subject.Name.ToString();
                            days[day - 1].DaySubjects[counter].LeadingTeachers.AddRange(subject.LeadingTeachers);
                            days[day - 1].DaySubjects[counter].Hours = 2;
                        }

                    }

                }
                ++counter;

            }
            foreach (ComboBox box in comboBoxes)
            {
                box.SelectedItem = null;
                box.Text = null;
            }


        }

        void ReadingDay()
        {
            if(days[day-1] != null)
            {
                for(int i = 0; i < 8; i++)
                    comboBoxes[i].Text = days[day-1].DaySubjects[i].Name;
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

    }
}
