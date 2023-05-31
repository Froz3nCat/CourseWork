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
    
    public partial class RepFrom : Form
    {
        
        public bool IsDeveloper = false;
        public string ConnectionString = null;
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommand sqlCommand = null;
        private DataSet dataSet = null;
        public RepFrom()
        {
            InitializeComponent();
        }

        private void ExecCommand(string command)
        {
            try {
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = command;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            
        }
        private void LoadDataSold()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Sold", sqlConnection);
                
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Sold");
                dataGridView1.DataSource = dataSet.Tables["Sold"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            LoadDataSold();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogresult = MessageBox.Show("Вы собираетесь отправить SQL-запрос, при неккоректном запросе, вернуть предыдущие значения таблицы будет невозможно",
                "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogresult == DialogResult.Yes)
            {
                IsDeveloper = true;
            }
            if(IsDeveloper)
            {
                string Exec = textBox1.Text.ToString();
                ExecCommand(Exec);
                IsDeveloper = false;
            }
            
            
        }

        
    }
}
