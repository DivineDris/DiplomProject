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
            foreach (Subject subject in subjects)
                listBox1.Items.Add(subject.Name);

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
    }
}
