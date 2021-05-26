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
        List<Teacher> teachers = new List<Teacher>();
        List<Subject> subjects = new List<Subject>();
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
    }
}
