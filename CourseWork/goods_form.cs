﻿using System;
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

        void sqlCommandExecute(string sql_command)
        {
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sql_command;
            sqlCommand.ExecuteNonQuery();

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
                            string id = dataGridView1.Rows[rowIndex].Cells["Id"].Value.ToString();
                            string sqlExec = "DELETE FROM Goods WHERE Id =" + id ;
                            //MessageBox.Show("ID", id);
                            sqlCommandExecute(sqlExec);
                            
                            /*
                            dataGridView1.Rows.RemoveAt(rowIndex);

                            dataSet.Tables["Goods"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, "Goods");

                            */
                        }
                    }

                    else if (task == "Insert")
                    {

                        int rowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Goods"].NewRow();


                        row["Наименование"] = dataGridView1.Rows[rowIndex].Cells["Наименование"].Value;
                        row["Кол-во"] = dataGridView1.Rows[rowIndex].Cells["Кол-во"].Value;
                        row["Цена"] = dataGridView1.Rows[rowIndex].Cells["Цена"].Value;
                        row["Сотрудник"] = dataGridView1.Rows[rowIndex].Cells["Сотрудник"].Value;
                        row["Дата"] = dataGridView1.Rows[rowIndex].Cells["Дата"].Value;
                        row["Секция_склада"] = dataGridView1.Rows[rowIndex].Cells["Секция_склада"].Value;

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


                        dataSet.Tables["Goods"].Rows[r]["Наименование"] = dataGridView1.Rows[r].Cells["Наименование"].Value;
                        dataSet.Tables["Goods"].Rows[r]["Кол-во"] = dataGridView1.Rows[r].Cells["Кол-во"].Value;
                        dataSet.Tables["Goods"].Rows[r]["Цена"] = dataGridView1.Rows[r].Cells["Цена"].Value;
                        dataSet.Tables["Goods"].Rows[r]["Сотрудник"] = dataGridView1.Rows[r].Cells["Сотрудник"].Value;
                        dataSet.Tables["Goods"].Rows[r]["Дата"] = dataGridView1.Rows[r].Cells["Дата"].Value;
                        dataSet.Tables["Goods"].Rows[r]["Секция_склада"] = dataGridView1.Rows[r].Cells["Секция_склада"].Value;


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
            comboBox1.SelectedItem = "-";
            
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
                    combo_value = null;
                    textBox2.ReadOnly = true;
                    textBox2.Text = "";
                    textBox2.Text = "Введите текст:";
                    LoadDataGoods();
                    break;
                case 1:
                    combo_value = "Наименование";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 2:
                    combo_value = "Сотрудник";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 3:
                    combo_value = "Секция_склада";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                    
 
            }
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (combo_value != null)
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"CONVERT({combo_value}, System.String) LIKE '%{textBox2.Text}%'";

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
