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

namespace labdb1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
        }

        private string connectionString = @"Data Source=DESKTOP-3G564KV\SQLEXPRESS;Initial Catalog = SupermarketQuery; Integrated Security = True";
        private SqlConnection connection;
        private DataSet dataSet;
        private SqlDataAdapter departmentsDA, employeesDA;
        private DataRelation fk_departments_employees;
        private BindingSource departmentsBS, employeesBS;
        private SqlCommandBuilder command;

        private void button3_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string id1 = textBox2.Text;

            string commandText = "DELETE FROM Employees WHERE EmployeeId =" + id + "AND DepartmentId =" + id1;
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

        private void button1_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            dataSet = new DataSet(); 
            departmentsDA = new SqlDataAdapter("Select * from Departments", connection);
            employeesDA = new SqlDataAdapter("Select * from Employees", connection);

            command = new SqlCommandBuilder(employeesDA);

            departmentsDA.Fill(dataSet,"Departments");
            employeesDA.Fill(dataSet, "Employees");

            fk_departments_employees = new DataRelation("fk_departments_employees", dataSet.Tables["Departments"].Columns["DepartmentId"], dataSet.Tables["Employees"].Columns["DepartmentId"]);
            dataSet.Relations.Add(fk_departments_employees);
            departmentsBS = new BindingSource();
            employeesBS = new BindingSource();

            departmentsBS.DataSource = dataSet;
            departmentsBS.DataMember = "Departments";

            employeesBS.DataSource = departmentsBS;
            employeesBS.DataMember = "fk_departments_employees";

            dataGridViewDepartaments.DataSource = departmentsBS;
            dataGridViewEmployees.DataSource = employeesBS;

        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            employeesDA.Update(dataSet, "Employees");
        }

        /*private void dataGridViewDepartaments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = dataGridViewDepartaments.SelectedCells[0].RowIndex;
            string s = dataGridViewDepartaments.Rows[d].Cells[0].Value.ToString();
            //MessageBox.Show(s);

            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Employees where DepartmentId="+s, conn);
            da1.Fill(ds1);
            dataGridViewEmployees.DataSource = ds1.Tables[0];

        }
        */
    }
}
