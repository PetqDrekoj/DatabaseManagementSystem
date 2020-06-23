using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            buildApp();
            addDatabaseConnections();
            addHandlers();
        }

        private string connectionString;
        private Form myForm;
        private DataGridView dataGridViewMaster;
        private DataGridView dataGridViewChild;
        private Button showDatabaseButton, updateDatabaseButton, deleteChildButton, changeDatabaseButton;
        private TextBox textBox1, textBox2;
        private SqlConnection connection;
        private DataSet dataSet;
        private SqlDataAdapter masterDA, childDA;
        private DataRelation fk;
        private BindingSource masterBS, childBS;
        private SqlCommandBuilder command;
        int dGVM_width = 400;
        int dGVM_height = 200;
        int dGVM_top = 50;
        int dGVM_left = 50;
        int dGVC_width = 400;
        int dGVC_height = 200;
        int dGVC_top = 50;
        int dGVC_left = 50;
        int bSD_width = 100;
        int bSD_height = 50;
        int bSD_top = 50;
        int bSD_left = 50;
        int bUD_width = 100;
        int bUD_height = 50;
        int bUD_top = 50;
        int bUD_left = 50;
        int bDC_width = 100;
        int bDC_height = 50;
        int bDC_top = 50;
        int bDC_left = 50;
        int bCD_width = 100;
        int bCD_height = 50;
        int bCD_top = 50;
        int bCD_left = 50;
        int t1_width = 50;
        int t1_height = 20;
        int t1_top = 50;
        int t1_left = 50;
        int t2_width = 50;
        int t2_height = 20;
        int t2_top = 50;
        int t2_left = 50;
        int left = 0;
        int top = 0;
        List<string> input;
        public void buildApp()
        {
            this.Size = new Size(1000, 500);
            left = this.Size.Width / 20;
            top = this.Size.Height / 80;

            input = new List<string>();
            foreach (string line in File.ReadLines(@"C:\Users\dyeni\Desktop\SDB\Lab2\Lab2\config.txt", Encoding.UTF8))
            {
                string[] args = line.Split(':');
                input.Add(args[1]);
            }

            showDatabaseButton = new Button();
            showDatabaseButton.Size = new Size(bSD_width, bSD_height);
            showDatabaseButton.Location = new Point(left + bSD_left, top + bSD_top);
            showDatabaseButton.Text = "Show Database";
            showDatabaseButton.BackColor = Color.Gray;

            updateDatabaseButton = new Button();
            updateDatabaseButton.Size = new Size(bUD_width, bUD_height);
            updateDatabaseButton.Location = new Point(left + bUD_left + bSD_left + bSD_width, top + bUD_top);
            updateDatabaseButton.Text = "Update Database";
            updateDatabaseButton.BackColor = Color.Gray;

            changeDatabaseButton = new Button();
            changeDatabaseButton.Size = new Size(bCD_width, bCD_height);
            changeDatabaseButton.Location = new Point(left + bUD_left + bSD_left + bSD_width + bUD_width + bCD_left, top + bUD_top);
            changeDatabaseButton.Text = "Swap Database";
            changeDatabaseButton.BackColor = Color.Gray;
            

            dataGridViewMaster = new DataGridView();
            dataGridViewMaster.Size = new Size(dGVM_width, dGVM_height);
            dataGridViewMaster.Location = new Point(left + dGVM_left, top + dGVM_top + bUD_top + bUD_height);
            

            dataGridViewChild = new DataGridView();
            dataGridViewChild.Size = new Size(dGVC_width, dGVC_height);
            dataGridViewChild.Location = new Point(left + dGVM_left + dGVC_left + dGVM_width, top + dGVC_top + bUD_top + bUD_height);
            

            deleteChildButton = new Button();
            deleteChildButton.Size = new Size(bDC_width, bDC_height);
            deleteChildButton.Location = new Point(left + bDC_left, top + bDC_top + dGVC_top + bUD_top + bUD_height + dGVC_height);
            deleteChildButton.Text = "Delete from Database";
            deleteChildButton.BackColor = Color.Gray;

            textBox1 = new TextBox();
            textBox1.Size = new Size(t1_width, t1_height);
            textBox1.Location = new Point(left + t1_left + bDC_left + bDC_width , top + t1_top + dGVC_top + bUD_top + bUD_height + dGVC_height);

            textBox2 = new TextBox();
            textBox2.Size = new Size(t2_width, t2_height);
            textBox2.Location = new Point(left + t2_left + t1_left + t1_width + bDC_left + bDC_width, top + t2_top + dGVC_top + bUD_top + bUD_height + dGVC_height);

            this.Controls.Add(dataGridViewMaster);
            this.Controls.Add(dataGridViewChild);
            this.Controls.Add(showDatabaseButton);
            this.Controls.Add(updateDatabaseButton);
            this.Controls.Add(changeDatabaseButton);
            this.Controls.Add(textBox1);
            this.Controls.Add(textBox2);
            this.Controls.Add(deleteChildButton);
        }

        private void addDatabaseConnections()
        {
            connectionString = input[0];
            connection = new SqlConnection(connectionString);
            dataSet = new DataSet();
            masterDA = new SqlDataAdapter("Select * from " + input[1], connection);
            childDA = new SqlDataAdapter("Select * from " + input[2], connection);

            command = new SqlCommandBuilder(childDA);

            masterDA.Fill(dataSet, input[1]);
            childDA.Fill(dataSet, input[2]);

            string idMasterColumnName = dataSet.Tables[input[1]].Columns[0].ToString();
            fk = new DataRelation("fk_"+ input[1] + "_" + input[2], dataSet.Tables[input[1]].Columns[idMasterColumnName], dataSet.Tables[input[2]].Columns[idMasterColumnName]);
            dataSet.Relations.Add(fk);
            masterBS = new BindingSource();
            childBS = new BindingSource();

            masterBS.DataSource = dataSet;
            masterBS.DataMember = input[1];

            childBS.DataSource = masterBS;
            childBS.DataMember = "fk_" + input[1] + "_" + input[2];

            dataGridViewMaster.DataSource = masterBS;
            dataGridViewChild.DataSource = childBS;


            dataGridViewChild.Columns[dataSet.Tables[input[1]].Columns[0].ToString()].ReadOnly = true;
        }

        private void showDatabaseClicked(object sender, EventArgs e)
        {
            addDatabaseConnections();
        }

        private void changeDatabaseClicked(object sender, EventArgs e)
        {
            String aux = input[1];
            input[1] = input[3];
            input[3] = aux;
            aux = input[2];
            input[2] = input[4];
            input[4] = aux;
            addDatabaseConnections();
        }

        private void updateDatabaseClicked(object sender, EventArgs e)
        {
            try
            {
                childDA.Update(dataSet, input[2]);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void deleteClientClicked(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string id1 = textBox2.Text;
            string idFieldNameMaster = dataSet.Tables[input[1]].Columns[0].ToString(); 
            string idFieldNameChild = dataSet.Tables[input[2]].Columns[0].ToString();

            string commandText = "DELETE FROM " + input[2] + " WHERE " + idFieldNameChild + "=" + id + " AND " + idFieldNameMaster + "=" + id1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(commandText, connection);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Success!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void addHandlers()
        {
            showDatabaseButton.Click += showDatabaseClicked;
            updateDatabaseButton.Click += updateDatabaseClicked;
            deleteChildButton.Click += deleteClientClicked;
            changeDatabaseButton.Click += changeDatabaseClicked;
        }
    }
}
