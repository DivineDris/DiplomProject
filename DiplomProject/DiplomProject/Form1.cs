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
    }
}
