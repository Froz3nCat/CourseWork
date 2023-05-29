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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CourseWork
{
    public partial class Sell : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        private string name = null;
        private int amount = 0;
        private double price = 0;
        private DateTime date;
        private int id = 0;
        private string combo_value = null;
        private DataGridView dt = null;
        public Sell()
        {
            InitializeComponent();
        }

        private void Sell_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(
               @"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;
                Integrated Security=True");
            sqlConnection.Open();

            LoadDataGoods();
        }
        private void LoadDataGoods()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *,'Sell' AS [Command] FROM Goods", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();

                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Goods");
                dataGridView1.DataSource = dataSet.Tables["Goods"];

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[7, i] = linkCell;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ReLoadDataGoods()
        {
            try
            {
                dataSet.Tables["Goods"].Clear();
                sqlDataAdapter.Fill(dataSet, "Goods");
                dataGridView1.DataSource = dataSet.Tables["Goods"];

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[7, i] = linkCell;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                String task = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (task == "Sell")
                {
                    price = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Цена"].Value);
                    amount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Кол-во"].Value);
                    id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                    name = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Наименование"].Value);
                    date = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["Дата"].Value);
                    SellForm goodBye = new SellForm();
                    goodBye.name = this.GetName;
                    goodBye.amount = this.GetAmount;
                    goodBye.Id = this.GetId;
                    goodBye.date = this.GetDate;
                    goodBye.price = this.getPrice;
                    goodBye.Show();
                    sqlConnection.Close();
                    
                }

            }
        }
        public double getPrice
        {
            get { return price; }
            set { price = value; }
        }
        public int GetAmount
        {
            get { return amount; }
            set { amount = value; }
        }
        public int GetId
        {
            get { return id; }
            set { id = value; }
        }
        public string GetName
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public DateTime GetDate
        {
            get { return date; }
            set
            {
                date = value;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case (0):
                    combo_value = null;
                    SortTextBox.ReadOnly = true;
                    SortTextBox.Text = "";
                    SortTextBox.Text = "Введите текст:";
                    LoadDataGoods();
                    break;
                case 1:
                    combo_value = "Наименование";
                    SortTextBox.ReadOnly = false;
                    SortTextBox.Text = "";
                    break;
                case 2:
                    combo_value = "Сотрудник";
                    SortTextBox.ReadOnly = false;
                    SortTextBox.Text = "";
                    break;
                case 3:
                    combo_value = "Секция_склада";
                    SortTextBox.ReadOnly = false;
                    SortTextBox.Text = "";
                    break;
            }
        }

        private void SortTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (combo_value != null)
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"CONVERT({combo_value}, System.String) LIKE '%{SortTextBox.Text}%'";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void обновитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataGoods();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
