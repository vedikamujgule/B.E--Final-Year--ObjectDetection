using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ObjectDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cameraInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.TopLevel = false;
            mf.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mf.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(mf);
            mf.Show();
        }

        private void preprocessImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainFrm mf = new MainFrm();
            mf.TopLevel = false;
            mf.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mf.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(mf);
            mf.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
