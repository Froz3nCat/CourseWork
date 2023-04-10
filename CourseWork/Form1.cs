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
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void w_button_Click(object sender, EventArgs e)
        {
            workers_form wf = new workers_form();
            wf.StartPosition = FormStartPosition.CenterScreen;
            wf.Show();
             
        }

        private void Form1_Load(object sender, EventArgs e)
        {            MessageBox.Show("'Курсовой проект' \n Создатель: Азаматов Дмитрий Алексеевич", "Курсовой проект \"Inventory manager\"", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void g_button_Click(object sender, EventArgs e)
        {
            goods_form gf = new goods_form();
            gf.StartPosition = FormStartPosition.CenterScreen;
            gf.Show();
        }

       
    }
}
