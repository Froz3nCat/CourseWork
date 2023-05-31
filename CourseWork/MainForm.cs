using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void инофрмацияОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" \"Inventory Manager\"\r\n" +
                "Разработчик: Азаматов Дмитрий Алексеевич\r\n" +
                "Студент Группы ПИМ-21-1\r\n", "Курсовой проект",
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void GoodsOpenLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GoodsForm gf = new GoodsForm();
            gf.StartPosition = FormStartPosition.CenterScreen;
            gf.Show();
        }

        private void WorkersOpenLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WorkersForm wf = new WorkersForm();
            wf.StartPosition = FormStartPosition.CenterScreen;
            wf.Show();
        }

        private void SellLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GoodsSellForm se = new GoodsSellForm();
            se.StartPosition = FormStartPosition.CenterScreen;
            se.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
