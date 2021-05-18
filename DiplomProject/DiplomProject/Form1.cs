using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void группаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupForm groupForm = new GroupForm();
            groupForm.Show();
        }

        private void предметToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubjectForm subjectForm = new SubjectForm();
            subjectForm.Show();
        }

        private void преподавательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeacherForm teacherForm = new TeacherForm();
            teacherForm.Show();
        }
    }
}
