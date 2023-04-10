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
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private SqlCommand sqlCommand = null;
        private DataSet dataSet = null;
        private bool newRowAdd = false;
        private bool IsInserted = true;
        public workers_form()
        {
            InitializeComponent();
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
            SoundPlayer simpleSound = new SoundPlayer(@"D:\С\Coursework\CourseWork\Sounds\Inserting.wav");
            simpleSound.Play();
        }

        private void workers_form_Load(object sender, EventArgs e)
        {
            
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\С\CourseWork\CourseWork\workers_database.mdf;Integrated Security=True");
            sqlConnection.Open();

            LoadDataWorkers();
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

                            dataGridView1.Rows.RemoveAt(rowIndex);

                            dataSet.Tables["Workers"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, "Workers");
                        }
                    }

                    else if (task == "Insert")
                    {

                        int rowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Workers"].NewRow();

                        row["name"] = dataGridView1.Rows[rowIndex].Cells["name"].Value;
                        row["position"] = dataGridView1.Rows[rowIndex].Cells["position"].Value;
                        row["number"] = dataGridView1.Rows[rowIndex].Cells["number"].Value;
                        row["email"] = dataGridView1.Rows[rowIndex].Cells["email"].Value;
                        row["age"] = dataGridView1.Rows[rowIndex].Cells["age"].Value;
                        row["dob"] = dataGridView1.Rows[rowIndex].Cells["dob"].Value;

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
                     
                       
                        dataSet.Tables["Workers"].Rows[r]["name"] = dataGridView1.Rows[r].Cells["name"].Value;
                        dataSet.Tables["Workers"].Rows[r]["position"] = dataGridView1.Rows[r].Cells["position"].Value;
                        dataSet.Tables["Workers"].Rows[r]["number"] = dataGridView1.Rows[r].Cells["number"].Value;
                        dataSet.Tables["Workers"].Rows[r]["email"] = dataGridView1.Rows[r].Cells["email"].Value;
                        dataSet.Tables["Workers"].Rows[r]["age"] = dataGridView1.Rows[r].Cells["age"].Value;
                        dataSet.Tables["Workers"].Rows[r]["dob"] = dataGridView1.Rows[r].Cells["dob"].Value;

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
                playSimpleSound();
                
                
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

        
    }
}
