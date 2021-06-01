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
    public partial class GroupForm : Form
    {
        string[] dep = { "В", "ВТ", "Д", "П", "Т", "Э" };
        List<GroupSubject> group_subjects = new List<GroupSubject>();
        List<Subject> subjects = new List<Subject>();
        List<Group> new_groups = new List<Group>();
        List<Teacher> all_teachers = new List<Teacher>();
        string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + "DataFiles\\Groups.bin";
        public GroupForm()
        {
            InitializeComponent();
        }

        private void GroupForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(dep);
            ReadingFile("DataFiles\\Teachers.bin");
            ReadingFile("DataFiles\\Subjects.bin");
            List<string> subjects_names = new List<string>();
            foreach(Subject subject in subjects)
                subjects_names.Add(subject.Name);
            comboBox2.Items.AddRange(subjects_names.ToArray());
                

        }

        void ReadingFile(string str)
        {
            switch (str)
            {
                case "DataFiles\\Teachers.bin":
                    string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                    Stream openFileStream = File.OpenRead(FileName);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    all_teachers = (List<Teacher>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    break;
                case "DataFiles\\Subjects.bin":
                    FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + str;
                    openFileStream = File.OpenRead(FileName);
                    deserializer = new BinaryFormatter();
                    subjects = (List<Subject>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    break;
            }
                
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                comboBox4.Visible = true;
            else
            {
                comboBox4.Visible = false;
                comboBox4.Items.Clear();
            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            foreach (Subject subject in subjects)
            {
                if(subject.Name == comboBox2.SelectedItem.ToString())
                {
                    List<Teacher> find_teachers = subject.LeadingTeachers;
                    List<string> teachers_names = new List<string>();
                        foreach (Teacher teacher in find_teachers)
                            teachers_names.Add(teacher.LastName + " " + teacher.FirstName + " " + teacher.FatherName);
                    comboBox3.Items.AddRange(teachers_names.ToArray());
                    comboBox4.Items.AddRange(teachers_names.ToArray());
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string teachers;
            int counter = listBox1.Items.Count;
            GroupSubject new_group_subject = new GroupSubject();
            if (comboBox4.SelectedItem != null)
                teachers = comboBox3.SelectedItem.ToString() + " " + comboBox4.SelectedItem.ToString();
            else
                teachers = comboBox3.SelectedItem.ToString();
            new_group_subject.Name = comboBox2.SelectedItem.ToString();
            new_group_subject.Hours = Convert.ToInt32(textBox2.Text);
            
            if(checkBox1.Checked == true)
            {
                string teachers_name = comboBox3.SelectedItem.ToString();
                string[] subs = teachers_name.Split(' ');
                foreach (Teacher teacher in all_teachers)
                {
                    if ((teacher.LastName == subs[0]) && (teacher.FirstName == subs[1]) && (teacher.FatherName == subs[2]))
                        new_group_subject.LeadingTeachers.Add(teacher);
                }
                teachers_name = comboBox4.SelectedItem.ToString();
                subs = teachers_name.Split(' ');
                foreach (Teacher teacher in all_teachers)
                {
                    if ((teacher.LastName == subs[0]) && (teacher.FirstName == subs[1]) && (teacher.FatherName == subs[2]))
                        new_group_subject.LeadingTeachers.Add(teacher);
                }
            }
            else
            {
                string teachers_name = comboBox3.SelectedItem.ToString();
                string[] subs = teachers_name.Split(' ');
                teachers_name = comboBox3.SelectedItem.ToString();
                foreach (Teacher teacher in all_teachers)
                {
                    if ((teacher.LastName == subs[0]) && (teacher.FirstName == subs[1]) && (teacher.FatherName == subs[2]))
                        new_group_subject.LeadingTeachers.Add(teacher);
                }
            }
            group_subjects.Add(new_group_subject);
            listBox1.Items.Add(counter+1 +" "+comboBox2.SelectedItem.ToString()+" "+ textBox2.Text+" "+ teachers);
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            textBox2.Text = null;
            checkBox1.Checked = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Group group = new Group();
            group.Dep = comboBox1.SelectedItem.ToString();
            group.Number = textBox1.Text;
            group.Year = Convert.ToInt32(textBox4.Text);
            group.Subjects.AddRange(group_subjects);
            new_groups.Add(group);
            comboBox1.SelectedItem = null;
            textBox1.Text = null;
            textBox4.Text = null;
            listBox1.Items.Clear();
            group_subjects.Clear();
        }

        private void GroupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(new_groups.Count > 0)
            {
                if (File.Exists(FileName))
                {
                    List<Group> saved_groups = new List<Group>();
                    Stream openFileStream = File.OpenRead(FileName);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    saved_groups = (List<Group>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    new_groups.AddRange(saved_groups);
                    SaveFile();
                }
                else
                {
                    SaveFile();
                }
            }
        }
        void SaveFile()
        {
            Stream SaveFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, new_groups);
            SaveFileStream.Close();
        }
    }
}
