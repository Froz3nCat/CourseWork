using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{

    public partial class SellForm : Form
    {
        
        SqlConnection sqlConnection = null;
        SqlCommandBuilder sqlCommandBuilder = null; 
        SqlCommand sqlCommand = null;
        
        public SellForm()
        {
            InitializeComponent();
        }
        
        public int Id = 0;
        public string name = null;
        public int amount = 0;
        public double price = 0;
        private int user = 0;
        private int SoldAmount = 0;
        private int OutAmount = 0;
        public DateTime TodayDate = DateTime.Today;
        public DateTime date ;
        private string UserComment = null;
        private bool isconverted = false;
        void sqlCommandExecute(string sql_command)
        {

            sqlCommand = new SqlCommand(sql_command, sqlConnection);
            sqlCommand.ExecuteNonQuery();

        }
        void ClearTable()
        {
            string clearExec = @"Delete FROM Goods WHERE [Кол-во] <= 0 ";
            sqlCommandExecute(clearExec);
        }
        private void GoodBye_Load(object sender, EventArgs e)
        {
            NameLabel.Text = Convert.ToString(name);
            DateLabel.Text = date.ToString("d");
            AmountLabel.Text = Convert.ToString(amount);
            PriceLabel.Text = Convert.ToString(price);
            sqlConnection = new SqlConnection(
               @"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;
                Integrated Security=True");
            
            sqlConnection.Open();
            
        }

        private void SendSold_Click(object sender, EventArgs e)
        {
            try { 
                SoldAmount = Convert.ToInt32(textBox1.Text);
                price = Convert.ToDouble(textBox2.Text);
                user = Convert.ToInt32(textBox3.Text); 
                UserComment = Convert.ToString(CommentTextBox.Text);
                Convert.ToInt32(textBox3.Text);

                if (SoldAmount > amount)
                {
                    MessageBox.Show("Значение превосходит количество остатков товара "+ name+" на складе", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    isconverted = false;
                }
                else
                {
                    isconverted = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Введите численное значение\n"+ex.Message,"Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

            if (isconverted) {
                
                OutAmount = amount - SoldAmount;
                price = price * SoldAmount;
                
                try
                {
                    string sqlExec2 = @"INSERT INTO Sold(Наименование,Продано,Сумма,Сотрудник,Дата,Комментарий) 
                    VALUES (N'" + name.ToString() + "', " + SoldAmount.ToString() + ",  " + price.ToString() + ", '"+user.ToString()+"', '"+TodayDate.ToString("yyyy-M-dd")+"', N'"+UserComment.ToString()+"')";
                    string sqlExec = "UPDATE Goods SET [Кол-во] =" + OutAmount + "WHERE Id =" + Id.ToString();
                    sqlCommandExecute(sqlExec);
                    sqlCommandExecute(sqlExec2);
                    amount = OutAmount;
                    if (amount ==0)
                    {
                        this.Close();
                        MessageBox.Show("Исчерпан лимит товара!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    else {
                        DialogResult dialogResult = MessageBox.Show("Желаете продолжить продажу данного товара?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            textBox1.Text = null; textBox2.Text = null; textBox3.Text = null; CommentTextBox.Text = null;
                            AmountLabel.Text = Convert.ToString(OutAmount);
                        }
                        else
                        {
                            this.Close();
                            
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка(SQL)", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                finally
                {
                    isconverted = false;
                    ClearTable();
                }
            }
            

           
        
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SellForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GoodsSellForm goodsSellForm = new GoodsSellForm();  
            goodsSellForm.StartPosition = FormStartPosition.CenterScreen;
            goodsSellForm.Show();

        }
    }

}

