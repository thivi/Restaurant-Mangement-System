using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResMan
{
    public partial class addCustomer : Form
    {
        public addCustomer()
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
         
            else
            {
                try
                {
                    
                    DB db = new DB();
                    if (db.checkNIC(nic.Text))
                    {
                        MessageBox.Show("The NIC number already exists!", "Duplicate content");
                    }
                    else
                    {
                        Boolean res = db.addCustomer(fname.Text, lname.Text, contactno.Text, nic.Text, dob.Value);
                        if (res)
                        {
                            DialogResult yn = MessageBox.Show("Customer successfully added! Do you want to add another?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
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
                        else
                        {
                            MessageBox.Show("An Error Occured", "Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            


        }

        private void addCustomer_Load(object sender, EventArgs e)
        {
            dob.MaxDate = DateTime.Today.AddYears(-6);
        }
    }
}
