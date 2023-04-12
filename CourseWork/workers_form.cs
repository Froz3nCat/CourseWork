using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CourseWork
{
    public partial class workers_form : Form
    {
     
        //ccc
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommand sqlCommand = null;
        private DataSet dataSet = null;
        private bool newRowAdd = false;
        private bool IsInserted = true;
        private string combo_value = null;
        public workers_form()
        {

            InitializeComponent();
            
        }
        void sqlCommandExecute(string sql_command)
        {
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sql_command;
            sqlCommand.ExecuteNonQuery();

        }
        private void LoadDataWorkers()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *,'Delete' AS [Command] FROM Workers", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();
                
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Workers");
                dataGridView1.DataSource = dataSet.Tables["Workers"];
                
                for(int i = 0; i< dataGridView1.RowCount; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[7, i] = linkCell;
                }
                
                
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message , "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ReLoadDataWorkers()
        {
            try
            {
                dataSet.Tables["Workers"].Clear();  

                sqlDataAdapter.Fill(dataSet, "Workers");

                dataGridView1.DataSource = dataSet.Tables["Workers"];

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
        private void playSimpleSound()
        {
        }

        private void workers_form_Load(object sender, EventArgs e)
        {
            
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;Integrated Security=True");
            sqlConnection.Open();

            LoadDataWorkers();
            comboBox1.SelectedItem = "-";
            
            
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReLoadDataWorkers();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex == 7)
                {
                    String task = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить выбранную строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            string id = dataGridView1.Rows[rowIndex].Cells["Id"].Value.ToString();
                            string sqlExec = "DELETE FROM Goods WHERE Id =" + id;
                            //MessageBox.Show("ID", id);
                            sqlCommandExecute(sqlExec);
                            /*
                            int rowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(rowIndex);

                            dataSet.Tables["Workers"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, "Workers");
                            */
                        }
                    }

                    else if (task == "Insert")
                    {

                        int rowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Workers"].NewRow();

                        row["ФИО"] = dataGridView1.Rows[rowIndex].Cells["ФИО"].Value;
                        row["Должность"] = dataGridView1.Rows[rowIndex].Cells["Должность"].Value;
                        row["Тел.номер"] = dataGridView1.Rows[rowIndex].Cells["Тел.номер"].Value;
                        row["Почта"] = dataGridView1.Rows[rowIndex].Cells["Почта"].Value;
                        row["Возраст"] = dataGridView1.Rows[rowIndex].Cells["Возраст"].Value;
                        row["Дата_рождения"] = dataGridView1.Rows[rowIndex].Cells["Дата_рождения"].Value;

                        dataSet.Tables["Workers"].Rows.Add(row);

                        dataSet.Tables["Workers"].Rows.RemoveAt(dataSet.Tables["Workers"].Rows.Count - 2);

                        //dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Workers");

                        newRowAdd = false;
                        
                       
                    }

                    else if (task == "Update")
                    {

                        int r = e.RowIndex; 
                     
                       
                        dataSet.Tables["Workers"].Rows[r]["ФИО"] = dataGridView1.Rows[r].Cells["ФИО"].Value;
                        dataSet.Tables["Workers"].Rows[r]["Должность"] = dataGridView1.Rows[r].Cells["Должность"].Value;
                        dataSet.Tables["Workers"].Rows[r]["Тел.номер"] = dataGridView1.Rows[r].Cells["Тел.номер"].Value;
                        dataSet.Tables["Workers"].Rows[r]["Почта"] = dataGridView1.Rows[r].Cells["Почта"].Value;
                        dataSet.Tables["Workers"].Rows[r]["Возраст"] = dataGridView1.Rows[r].Cells["Возраст"].Value;
                        dataSet.Tables["Workers"].Rows[r]["Дата_рождения"] = dataGridView1.Rows[r].Cells["Дата_рождения"].Value;

                        sqlDataAdapter.Update(dataSet, "Workers");
                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";
                    }

                    LoadDataWorkers();                
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        
         private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                newRowAdd = true;                  
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow editingTow = dataGridView1.Rows[rowIndex];
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                dataGridView1[7, rowIndex] = linkCell;
                editingTow.Cells["Command"].Value = "Update";
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
              
                sqlDataAdapter.Update(dataSet, "Workers");
                
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    combo_value = null;
                    textBox2.ReadOnly = true;
                    textBox2.Text = "";
                    textBox2.Text = "Введите текст:";
                    LoadDataWorkers();
                    break;
                case 1:
                    combo_value = "Id";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 2:
                    combo_value = "ФИО";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 3:
                    combo_value = "Должность";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 4:
                    combo_value = "Возраст";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.ReadOnly == true)
            {
                MessageBox.Show("Выберите критерии сортировки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void workers_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
