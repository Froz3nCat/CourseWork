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
using System.Xml.Linq;

namespace CourseWork
{
    public partial class goods_form : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlConnection nrthwndConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommand sqlCommand = null;
        private DataSet dataSet = null;
        private bool newRowAdd = false;
        private bool IsInserted = true;
        private SqlDataReader sqlDataReader = null;
        private List<string[]> rows;
        private string combo_value = null;
        public goods_form()
        {
            InitializeComponent();
        }
        private void LoadDataGoods()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *,'Delete' AS [Command] FROM Goods", sqlConnection);
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
        private void LoadRefresedRows()
        {
            try
            {

            }
            catch(Exception ex) {
                
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    String task = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    /*
                     * !!!!!!!!!!!!!ПЕРЕПИСАТЬ УДАЛЕНИЕ ИЗ ТАБЛИЦЫ
                     * УДАЛЯТЬ ИЗ БД T-SQL - ПОДГРУЖАТЬ с помощью LoadDataGoods();
                     */
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить выбранную строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(rowIndex);

                            dataSet.Tables["Goods"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, "Goods");
                        }
                    }

                    else if (task == "Insert")
                    {

                        int rowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Goods"].NewRow();


                        row["name"] = dataGridView1.Rows[rowIndex].Cells["name"].Value;
                        row["amount"] = dataGridView1.Rows[rowIndex].Cells["amount"].Value;
                        row["price"] = dataGridView1.Rows[rowIndex].Cells["price"].Value;
                        row["worker"] = dataGridView1.Rows[rowIndex].Cells["worker"].Value;
                        row["date"] = dataGridView1.Rows[rowIndex].Cells["date"].Value;
                        row["location"] = dataGridView1.Rows[rowIndex].Cells["location"].Value;

                        dataSet.Tables["Goods"].Rows.Add(row);

                        dataSet.Tables["Goods"].Rows.RemoveAt(dataSet.Tables["Goods"].Rows.Count - 2);

                        //dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Goods");

                        newRowAdd = false;


                    }

                    else if (task == "Update")
                    {

                        int r = e.RowIndex;


                        dataSet.Tables["Goods"].Rows[r]["name"] = dataGridView1.Rows[r].Cells["name"].Value;
                        dataSet.Tables["Goods"].Rows[r]["amount"] = dataGridView1.Rows[r].Cells["amount"].Value;
                        dataSet.Tables["Goods"].Rows[r]["price"] = dataGridView1.Rows[r].Cells["price"].Value;
                        dataSet.Tables["Goods"].Rows[r]["worker"] = dataGridView1.Rows[r].Cells["worker"].Value;
                        dataSet.Tables["Goods"].Rows[r]["date"] = dataGridView1.Rows[r].Cells["date"].Value;
                        dataSet.Tables["Goods"].Rows[r]["location"] = dataGridView1.Rows[r].Cells["location"].Value;


                        sqlDataAdapter.Update(dataSet, "Goods");
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";
                    }

                    LoadDataGoods();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];
                DataGridViewLinkCell linkcell = new DataGridViewLinkCell();
                dataGridView1[7, rowIndex] = linkcell;
                editingRow.Cells["Command"].Value = "Update";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void goods_form1_Load(object sender, EventArgs e)
        {
            
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;Integrated Security=True");
            sqlConnection.Open();
            
            LoadDataGoods();

            
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReLoadDataGoods();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex){
                case 0:
                    combo_value = "name";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 1:
                    combo_value = "worker";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 2:
                    combo_value = "location";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                    
 
            }
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (combo_value == "name") 
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"name LIKE '%{textBox2.Text}%'";

                }
                else if (combo_value == "worker")
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"worker LIKE '%{textBox2.Text}%'";
                }
                else if (combo_value == "location")
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"location LIKE '%{textBox2.Text}%'";
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.ReadOnly == true)
            {
                MessageBox.Show("Выберите критерии сортировки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
