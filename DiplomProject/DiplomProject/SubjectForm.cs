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
    public partial class SubjectForm : Form
    {
        List<Teacher> teachers = new List<Teacher>();
        List<Subject> new_subjects = new List<Subject>();
        string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + "DataFiles\\Subjects.bin";

        public SubjectForm()
        {
            InitializeComponent();
        }

        void SaveFile()
        {
            Stream SaveFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, new_subjects);
            SaveFileStream.Close();
        }

        void ReadingTeachersFile()
        {
            string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + "DataFiles\\Teachers.bin";
            Stream openFileStream = File.OpenRead(FileName);
            BinaryFormatter deserializer = new BinaryFormatter();
            teachers = (List<Teacher>)deserializer.Deserialize(openFileStream);
            openFileStream.Close();
        }



        private void SubjectForm_Load(object sender, EventArgs e)
        {
            ReadingTeachersFile();
            foreach (Teacher teacher in teachers)
                listBox1.Items.Add(teacher.LastName+" "+teacher.FirstName+" "+teacher.FatherName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Subject subject = new Subject();
            subject.Name = textBox1.Text;
            foreach(string lb2 in listBox2.Items)
                {
                    foreach (Teacher teacher in teachers)
                    {
                        string str = teacher.LastName + " " + teacher.FirstName + " " + teacher.FatherName;
                        if (lb2 == str)
                        {
                            subject.LeadingTeachers.Add(teacher);
                            break;
                        }
                            
                    }
                }
                
            new_subjects.Add(subject);
            textBox1.Text = null;
            listBox2.Items.Clear();
        }


        private void SubjectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(new_subjects.Count > 0)
            {
                if (File.Exists(FileName))
                {
                    List<Subject> saved_subjects = new List<Subject>();
                    Stream openFileStream = File.OpenRead(FileName);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    saved_subjects = (List<Subject>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    new_subjects.AddRange(saved_subjects);
                    SaveFile();
                }
                else
                {
                    SaveFile();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }
    }
}
