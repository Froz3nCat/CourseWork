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
            MessageBox.Show(" \"Inventory Manager\"\r\n\n" +
                "\tРазработчик: Азаматов Дмитрий Алексеевич\r\n" +
                "\tСтудент группы ПИМ-21-1\r\n"+
                "\tМининский университет 2023", "Курсовой проект",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
