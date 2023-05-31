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
    public partial class WorkersForm : Form
    {

        private string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;Integrated Security=True";
        private SqlConnection sqlConnection = null; 
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommand sqlCommand = null;
        private DataSet dataSet = null;
        
        private string ComboValue = null;
        private bool is_empty;
        private int date_row;
        private int date_col;

        private string SqlTableName = "Workers";
        private string[] TableHeaders = { "ФИО", "Должность", "Тел.номер", "Почта", "Возраст", "Дата_рождения" };
        public WorkersForm()
        {
            InitializeComponent();
        }

        private bool sqlCommandExecute(string sql_command)
        {
            try
            {
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sql_command;

                int ComparedStrings = sqlCommand.ExecuteNonQuery();
                if (ComparedStrings > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        private bool RowCompleded(int r)
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Rows[r].Cells[i].Value.ToString() == "")
                {
                    return false;
                }
            }
            return true;
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
                sqlDataAdapter.Fill(dataSet, SqlTableName);
                dataGridView1.DataSource = dataSet.Tables[SqlTableName];
                
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
                dataSet.Tables[SqlTableName].Clear();  
                sqlDataAdapter.Fill(dataSet, SqlTableName);
                dataGridView1.DataSource = dataSet.Tables[SqlTableName];

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

        private void workers_form_Load(object sender, EventArgs e)
        {
            try {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();
                LoadDataWorkers();

                comboBox1.SelectedItem = "-";
                textBox2.ReadOnly = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateByDataSet(int r)
        {
            for (int i = 0; i < TableHeaders.Length; i++)
            {
                dataSet.Tables[SqlTableName].Rows[r][TableHeaders[i]] = dataGridView1.Rows[r].Cells[TableHeaders[i]].Value;
            }
            sqlDataAdapter.Update(dataSet, SqlTableName);
            dataGridView1.Rows[r].Cells[7].Value = "Delete";
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
                    int rowIndex = e.RowIndex;

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить выбранную строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {     
                            string id = dataGridView1.Rows[rowIndex].Cells["Id"].Value.ToString();
                            string sqlExec = "DELETE FROM Workers WHERE Id =" + id;
                            sqlCommandExecute(sqlExec);
                        }
                    }

                    else if (task == "Update")
                    {
                        if (RowCompleded(rowIndex))
                        {
                            UpdateByDataSet(rowIndex);
                        }
                        else
                        {
                            MessageBox.Show("Заполните все значиения строки заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
                    ComboValue = null;
                    textBox2.ReadOnly = true;
                    textBox2.Text = "";
                    textBox2.Text = "Введите текст:";
                    LoadDataWorkers();
                    break;
                case 1:
                    ComboValue = "Id";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 2:
                    ComboValue = "ФИО";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 3:
                    ComboValue = "Должность";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
                case 4:
                    ComboValue = "Возраст";
                    textBox2.ReadOnly = false;
                    textBox2.Text = "";
                    break;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ComboValue != null)
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"CONVERT({ComboValue}, System.String) LIKE '%{textBox2.Text}%'";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex != dataGridView1.RowCount-1)
            {
                
                date_col = e.ColumnIndex;
                date_row = e.RowIndex;
                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;
                int formx = this.Location.X;
                int formy = this.Location.Y;
                monthCalendar1.Location = new Point(x - formx-20, y - formy-20);
                monthCalendar1.Visible = true;
                monthCalendar1.Enabled = true;

            }
            else
            {
                monthCalendar1.Visible = false;
                monthCalendar1.Enabled = false;
            }
        }

       
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {

            if (monthCalendar1.SelectionRange.Start <= monthCalendar1.TodayDate)
            {
                dataGridView1.Rows[date_row].Cells[date_col].Value = monthCalendar1.SelectionRange.Start.ToShortDateString();
                monthCalendar1.Visible = false;
                monthCalendar1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Невозможно выбрать дату. Выбранная вами дата еще не наступила.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
