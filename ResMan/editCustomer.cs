using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResMan
{
    public partial class editCustomer : Form
    {
        int custid=-1;
        public editCustomer()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void addCust_Click(object sender, EventArgs e)
        {
            Regex fn = new Regex(@"[a-zA-Z]*");
            Regex cn = new Regex(@"[\d][\d][\d]-[\d]{7}");
            Regex ni = new Regex(@"[\d]{9}[Vv]");

            if (fname.Text =="")
            {
                MessageBox.Show("Fist Name cannot be empty!");
            }
            else if (!fn.IsMatch(fname.Text))
            {
                MessageBox.Show("First Name can contain only alphabets!");
            }
            else if (lname.Text =="")
            {
                MessageBox.Show("Last Name cannot be empty!");
            }
            else if (!fn.IsMatch(lname.Text))
            {
                MessageBox.Show("Last Name can contain only alphabets!");
            }
            else if (contactno.Text == "")
            {
                MessageBox.Show("Contact Number cannot be empty!");
            }
            else if (!cn.IsMatch(contactno.Text))
            {
                MessageBox.Show("Contact Number can have only digits and should be of the following format: xxx-xxxxxxx!");


            }
            else if (nic.Text =="")
            {
                MessageBox.Show("NIC number cannot be empty!");
            }
            else if (!ni.IsMatch(nic.Text))
            {
                MessageBox.Show("NIC Number format is incorrect!");
            }
            else if (nic.TextLength != 10)
            {
                MessageBox.Show("NIC Number should contain 10 characters!");
            }
            else if (custid == -1)
            {
                MessageBox.Show("Customer ID has not been set!");
            }
         
            else
            {
                try
                {
                    
                    DB db = new DB();
                    if (db.checkNIC(nic.Text) && db.getCustID(nic.Text)!=custid)
                    {
                        MessageBox.Show("The NIC number already exists!", "Duplicate content");
                    }
                    else
                    {

                            db.updateTable("update Customers set FirstName='" + fname.Text + "', LastName='" + lname.Text + "', ContactNo='" + contactno.Text + "', NICno='" + nic.Text + "', DOB='" + dob.Value + "' WHERE CustomerID="+custid);
                        dataGridView1.DataSource = db.getTables("select CustomerID as 'ID', FirstName as 'First Name', LastName as 'Last Name', ContactNo, NICno as 'NIC', DOB, RegDate as 'Registered Date'from Customers");

                        DialogResult yn = MessageBox.Show("Customer successfully edited! Do you want to edit another?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (yn == DialogResult.Yes)
                            {
                                fname.Clear();
                                lname.Clear();
                                contactno.Clear();
                                nic.Clear();
                                dob.ResetText();
                            }
                            else
                            {
                                this.Close();
                            }
                        
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void editCustomer_Load(object sender, EventArgs e)
        {
            DB db = new ResMan.DB();
            dataGridView1.DataSource = db.getTables("select CustomerID as 'ID', FirstName as 'First Name', LastName as 'Last Name', ContactNo, NICno as 'NIC', DOB, RegDate as 'Registered Date'from Customers");
            dob.MaxDate = DateTime.Today.AddYears(-6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rx = new Regex(@"[\d]+");
            if (textBox1.Text == "")
            {
                MessageBox.Show("Customer ID is empty!");
            }
            else if (!rx.IsMatch(textBox1.Text))
            {
                MessageBox.Show("Customer ID can only contain numbers!");
            }
            else
            {
                int id = Convert.ToInt32(textBox1.Text);
                DB db = new DB();
                dataGridView1.DataSource = db.getTables("select CustomerID as 'ID', FirstName as 'First Name', LastName as 'Last Name', ContactNo, NICno as 'NIC', DOB, RegDate as 'Registered Date'from Customers WHERE CustomerID=" + id);
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("The customer for the entered ID is not found!");
                }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                foreach(DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    custid = Convert.ToInt32(item.Cells[0].Value);
                    fname.Text = item.Cells[1].Value.ToString();
                    lname.Text = item.Cells[2].Value.ToString();
                    contactno.Text = item.Cells[3].Value.ToString();
                    nic.Text = item.Cells[4].Value.ToString();
                    dob.Value = Convert.ToDateTime(item.Cells[5].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a row from the table!");
                }
                else
                {
                    foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(item.Cells[0].Value);
                        DB db = new ResMan.DB();
                        DialogResult res = MessageBox.Show("Are you sure that you want to remove?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            db.updateTable("delete from Customers where CustomerID=" + id);
                            dataGridView1.DataSource = db.getTables("select CustomerID as 'ID', FirstName as 'First Name', LastName as 'Last Name', ContactNo, NICno as 'NIC', DOB, RegDate as 'Registered Date'from Customers");
                            MessageBox.Show("Customer removed!");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to delete since that would affect the already existing records.");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = addCust;
        }
    }
}
