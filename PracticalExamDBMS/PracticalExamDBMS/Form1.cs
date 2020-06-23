using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace PracticalExamDBMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // here I set the connection string, the data source is telling info about my sql username, 
        // initital catalog refers to the Database that I am using.
        private string connectionString = @"Data Source=DESKTOP-3G564KV\SQLEXPRESS;Initial Catalog = atmTransaction; Integrated Security = True";

        // here I just declared the things we will need for this project.
        
       // Sql Connection which is an object that stores info about the connection like a file descriptor. 
        private SqlConnection connection;
        // the DataSet is where data is actually kept
        private DataSet dataSet;
        // the data adapters provide the data source connection and the ability to perform disconnected operations.
        private SqlDataAdapter parentDA, childDA;
        // this is just a realtion object that we will use in order to specify that the child has as foreign key a pk from the parent table
        private DataRelation fk_parent_child;
        // the binding source acts like a data source here
        private BindingSource parentBS, childBS;
        private SqlCommandBuilder command;



        private void button1_Click(object sender, EventArgs e)
        {
            //here we create the connection
            connection = new SqlConnection(connectionString);
            dataSet = new DataSet();

            //here we fetch the data in the adapters.
            parentDA = new SqlDataAdapter("Select * from customers", connection);
            childDA = new SqlDataAdapter("Select * from cards", connection);

            command = new SqlCommandBuilder(childDA);

            // but here in the dataSet it is stored the result from the adapters.
            parentDA.Fill(dataSet, "customers");
            childDA.Fill(dataSet, "cards");

            // we just specify the foreign key, so the constraint
            fk_parent_child = new DataRelation("fk_parent_child", dataSet.Tables["customers"].Columns["cid"], dataSet.Tables["cards"].Columns["cid"]);
            dataSet.Relations.Add(fk_parent_child);
            
            
            parentBS = new BindingSource();
            childBS = new BindingSource();

            // here I just bind the data to the correct member
            parentBS.DataSource = dataSet;
            parentBS.DataMember = "customers";

            childBS.DataSource = parentBS;
            childBS.DataMember = "fk_parent_child";

            /// here I bind the dataGridViews to the sources that I have.
            dgvCustomers.DataSource = parentBS;
            dgvCards.DataSource = childBS;

            // this is just so that the user cannot change the foreign key from the child table.
            dgvCards.Columns["cid"].ReadOnly = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this is where I send back to the database the updates through the data set.
            try
            {
                childDA.Update(dataSet, "cards");
            }
            catch (Exception)
            {
                MessageBox.Show("The bank id couldn't be found");
                
            }
            

        }
    }
}
