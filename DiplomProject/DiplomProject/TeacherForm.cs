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
    public partial class TeacherForm : Form
    {
        List<Teacher> new_teachers = new List<Teacher>();
        string FileName = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + "DataFiles\\Teachers.bin";
        public TeacherForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Teacher teacher = new Teacher();
            teacher.FirstName = textBox2.Text;
            teacher.LastName = textBox1.Text;
            teacher.FatherName = textBox3.Text;
            new_teachers.Add(teacher);
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }
        void SaveFile()
        {
            Stream SaveFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, new_teachers);
            SaveFileStream.Close();
        }

        private void TeacherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (new_teachers.Count > 0)
            {
                if (File.Exists(FileName))
                {
                    List<Teacher> saved_teachers = new List<Teacher>();
                    Stream openFileStream = File.OpenRead(FileName);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    saved_teachers = (List<Teacher>)deserializer.Deserialize(openFileStream);
                    openFileStream.Close();
                    new_teachers.AddRange(saved_teachers);
                    SaveFile();
                }
                else
                {
                    SaveFile();
                }
            }

        }
    }
}
