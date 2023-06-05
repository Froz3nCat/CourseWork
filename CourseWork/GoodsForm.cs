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
        private void LocalUpdate(int RowIndex)//Обновление информации в таблице
        {
            //заполняется объект DataSet, информация берется из datagridview
            for (int i = 0; i < TableHeaders.Length; i++)
            {
                dataSet.Tables[SqlTableName].Rows[RowIndex][TableHeaders[i]] = dataGridView1.Rows[RowIndex].Cells[TableHeaders[i]].Value;
            }
            sqlDataAdapter.Update(dataSet, SqlTableName);//заполнение таблицы в базе данных
            dataGridView1.Rows[RowIndex].Cells[7].Value = "Delete";
        }

        private bool RowCompleded(int RowIndex)
                                               //проверка полноты информации в строке
        {                                      //если строка не заполнена, или заполнена частично,
                                               //то возвращает значение False
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Rows[RowIndex].Cells[i].Value.ToString() == "")
                {
                    return false;
                }
            }
            return true;
        }

        private bool sqlCommandExecute(string sql_command)//функция обработки SQL-запросов
            //возвращает булево, определяющее была-ли выполнена команда
        {
            try
            {
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sql_command;

                int ComparedStrings = sqlCommand.ExecuteNonQuery();
                //отправка команды на выполнение, возвращает кол-во рядов, удовлетворяющих условию

                if (ComparedStrings > 0)//если по заданному условию обработано более нуля рядов
                {
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        private void LoadDataGoods() //загрузка информации из базы данных в DataGridViev
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *,'Delete' AS [Command] FROM Goods", sqlConnection);//Создание запроса
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();

                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, SqlTableName);//заполнение датасета(посредника)
                dataGridView1.DataSource = dataSet.Tables[SqlTableName];//заполнение dataGridView
                
                for (int i = 0; i < dataGridView1.RowCount; i++)//Создание текста со ссылкой в 7 столбце таблицы
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[7, i] = linkCell;
                }
                if (dataGridView1.Rows.Count == 1)//Вывод на центр форм информации о том, что склад пуст
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

        private void ReLoadDataGoods()//обновление информации в datagridview
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
            //обработка события нажатия на ячейку таблицы
        {
            try
            {
                if (e.ColumnIndex == 7)//если нажали на ячейку с командой
                {
                    String task = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    //отслеживаем значение ячейки
                    int rowIndex = e.RowIndex;
                    //отслеживаем индекс ряда
                    if (task == "Delete")
                    {
                        DialogResult dialogResult = MessageBox.Show("Удалить выбранную строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                        if (dialogResult == DialogResult.Yes)
                        {
                            string id = dataGridView1.Rows[rowIndex].Cells["Id"].Value.ToString();
                            //удаление происходит через уникальный идентификатор(ID)
                            string sqlExec = "DELETE FROM Goods WHERE Id =" + id;
                            //Команда на удаление рядов
                            sqlCommandExecute(sqlExec);
                            //отправка команды на выполнение
                        }
                    }
                    else if (task == "Update")
                    {
                        if (RowCompleded(rowIndex))//если ряд зполнен, то обновляем ифнформацию
                        {
                            LocalUpdate(rowIndex);     
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
            //Данная функция отслеживает изменение значения в табилице, 
            //если хотя бы одно изменено, то к измененному значению в ряде, предложится обновить данные
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
                sqlConnection = new SqlConnection(ConnectionString);//создание подключения
                sqlConnection.Open();//открытие подключения
                LoadDataGoods();//подгружаем информацию из таблиц
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
            //обработка собитыя выбора критерия фильтрации, дополнительно создается 
            //переменная combo value, которая хранит данный критерий
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
            //функция задает фильтр по параметру, учитывая критерий 
        {
            try
            {
                if (combo_value != null)//фильтрация по критерию combo_value и параметру который вводит пользователь
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
            //данная функция отвечает за вывод обекта MonthCalendar при выборе даты
            if (e.ColumnIndex == 5 && e.RowIndex != dataGridView1.RowCount - 1)
                //если колонна соответствует колонне с выбором даты
            {
                date_col = e.ColumnIndex;
                date_row = e.RowIndex;
                //получаем информацию о ряде столбце

                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;
                //отслеживаем позицию курсора
                int FormX = this.Location.X;
                int FormY = this.Location.Y;
                //отслеживаем позицию формы
                monthCalendar1.Location = new Point(x - FormX - 20, y - FormY - 20);
                monthCalendar1.Visible = true;
                monthCalendar1.Enabled = true;
                //отображаем MonthCalendar
            }
            else
            {
                monthCalendar1.Visible = false;
                monthCalendar1.Enabled = false;
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            //Перенос информации из MonthCalendar
            if (monthCalendar1.SelectionRange.Start <= monthCalendar1.TodayDate) 
            {//выбранная дата не превосходит текущаю
                dataGridView1.Rows[date_row].Cells[date_col].Value = monthCalendar1.SelectionRange.Start.ToShortDateString();
                //заполнение значения в datagridview
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
            //Открытие формы с отчетом 
            RepFrom ReportForm = new RepFrom();
            ReportForm.ConnectionString = GetConnection;//передаем информацию о подключении
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