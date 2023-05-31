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
    public partial class GoodsForm : Form
    {
        private string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;
                Integrated Security=True";
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommand sqlCommand = null;
        private DataSet dataSet = null;
        private string combo_value = null;
        private int date_col = 0;
        private int date_row = 0;
        private string SqlTableName = "Goods";
        private string[] TableHeaders = { "Наименование", "Кол-во", "Цена", "Сотрудник", "Дата", "Секция_склада" };
        

        public GoodsForm()
        {
            InitializeComponent();
        }
        private void UpdateRow(int r)
        {
            for (int i = 0; i < TableHeaders.Length; i++)
            {
                dataSet.Tables[SqlTableName].Rows[r][TableHeaders[i]] = dataGridView1.Rows[r].Cells[TableHeaders[i]].Value;
            }
            sqlDataAdapter.Update(dataSet, SqlTableName);
            dataGridView1.Rows[r].Cells[7].Value = "Delete";
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

        private bool IsUser(string id)
        {
            string ExecCommand = @"SELECT * FROM Workers WHERE Id = "+id ;
            return sqlCommandExecute(ExecCommand);
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
                sqlDataAdapter.Fill(dataSet, SqlTableName);
                dataGridView1.DataSource = dataSet.Tables[SqlTableName];
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[7, i] = linkCell;
                }
                if (dataGridView1.Rows.Count == 1)
                {
                    InfoLabel.Text = "Склад пуст:(\nСкорее добавим что-нибудь!";
                    InfoLabel.Visible = true;
                }
                else
                {
                    InfoLabel.Visible = false;
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
                dataSet.Tables[SqlTableName].Clear();
                sqlDataAdapter.Fill(dataSet, SqlTableName);
                dataGridView1.DataSource = dataSet.Tables[SqlTableName];
                MessageBox.Show(dataGridView1.Rows.Count.ToString());
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[7, i] = linkCell;
                }
                if (dataGridView1.Rows.Count == 1)
                {
                    InfoLabel.Text = "Склад пуст:(\nСкорее добавим что-нибудь!";
                    InfoLabel.Visible = true;
                }
                else
                {
                    InfoLabel.Visible = false;

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
                    int rowIndex = e.RowIndex;

                    if (task == "Delete")
                    {
                        DialogResult dialogResult = MessageBox.Show("Удалить выбранную строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                        if (dialogResult == DialogResult.Yes)
                        {
                            string id = dataGridView1.Rows[rowIndex].Cells["Id"].Value.ToString();
                            string sqlExec = "DELETE FROM Goods WHERE Id =" + id;
                            sqlCommandExecute(sqlExec);
                        }
                    }

                    else if (task == "Update")
                    {
                        if (RowCompleded(rowIndex))
                        {
                                UpdateRow(rowIndex);     
                        }
                        else
                        {
                            MessageBox.Show("Заполните все значиения строки заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
            try 
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();
                LoadDataGoods();
                comboBox1.SelectedItem = "-";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReLoadDataGoods();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            sqlConnection.Close();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.ReadOnly)
            {
                MessageBox.Show("Выберите критерии сортировки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex != dataGridView1.RowCount - 1)
            {
                date_col = e.ColumnIndex;
                date_row = e.RowIndex;

                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;
                int FormX = this.Location.X;
                int FormY = this.Location.Y;

                monthCalendar1.Location = new Point(x - FormX - 20, y - FormY - 20);
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

        private void отчетПоПродажамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RepFrom ReportForm = new RepFrom();
            ReportForm.ConnectionString = GetConnection;
            ReportForm.ShowDialog();

        }

        private string GetConnection
        {
            get { return ConnectionString; }
            set { ConnectionString = value; }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            InfoLabel.Visible = false;
        }

        
    }
}