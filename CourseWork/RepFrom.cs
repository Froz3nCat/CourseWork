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
    
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommand sqlCommand = null;
        private DataSet dataSet = null;
        public RepFrom()
        {
            InitializeComponent();
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
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;
                Integrated Security=True");
            sqlConnection.Open();
            LoadDataSold();
        }

       
    }
}
