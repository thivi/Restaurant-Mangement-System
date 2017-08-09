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
    public partial class editStaffs : Form
    {
        int staffid=-1;
        string pic;
        Bitmap initial;
        public editStaffs()
        {
            
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string privi="Assistant";
            
            Regex fn = new Regex(@"[a-zA-Z]*");
            Regex cn = new Regex(@"[\d][\d][\d]-[\d]{7}");
            Regex passd = new Regex(@"\d");
            Regex passa = new Regex(@"[a-z]");
            Regex passA = new Regex(@"[A-Z]");
            Regex passS = new Regex(@"\W");

            if (Admin.Checked)
            {
                privi = "Admin";
            }
            else if (Assistant.Checked)
            {
                privi = "Assistant";
            }

            if (fname.Text == "")
            {
                MessageBox.Show("First Name cannot be empty");
            }
            else if (!fn.IsMatch(fname.Text))
            {
                MessageBox.Show("First Name can contain only alphabets!");
            }
            else if (!fn.IsMatch(lname.Text))
            {
                MessageBox.Show("Last Name can contain only alphabets!");
            }
            else if (lname.Text == "")
            {
                MessageBox.Show("Last Name cannot be empty!");
            }
            else if (contactno.Text == "")
            {
                MessageBox.Show("Contact Number cannot be empty!");
            }
            else if (!cn.IsMatch(contactno.Text))
            {
                MessageBox.Show("Contact Number can have only digits and should be of the following format: xxx-xxxxxxx!");

            }
            else if(!Admin.Checked && !Assistant.Checked)
            {
                MessageBox.Show("Privelege Level has not been chosen!");
            }
          
            else if (uname.Text == "")
            {
                MessageBox.Show("User Name cannot be empty!");
            }
           
            else if (pwd.Text != "" && pwd.Text.Length < 6)
            {
                MessageBox.Show("Password must contain more than 6 characters");
            }
            else if (pwd.Text != "" && (!passA.IsMatch(pwd.Text) || !passa.IsMatch(pwd.Text) || !passd.IsMatch(pwd.Text) || !passS.IsMatch(pwd.Text)))
            {
                MessageBox.Show("Password must contain atleast one simple lowercase alphabet, uppercase alphabet ,number and a special character!");
            }
            else if (pwd.Text != "" &&  pwd.Text != confirm.Text)
            {
                MessageBox.Show("Passwords you entered does not match each other!");
            }
            else if (staffid == -1)
            {
                MessageBox.Show("Staff ID has not been set!");
            }
            else
            {
                try
                {

                    DB db = new DB();
                    
                    if (db.checkUname(uname.Text) && staffid != db.getStaffID(uname.Text))
                    {
                          MessageBox.Show("Username alrady exists!", "Username already exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        
                    }
                    else
                    {
                            db.updateTable("Update Staffs set FirstName='" + fname.Text + "', LastName='" + lname.Text + "', ContactNo='" + contactno.Text + "', PrivilegeLevel='" + privi + "', DOB='" + dob.Value + "', uname='" + uname.Text + "', picture='"+pic+"' WHERE StaffID=" + staffid);
                            dataGridView1.DataSource = db.getTables("select StaffID  as ID, FirstName  as 'First Name', LastName as 'Last Name', ContactNo as 'Contact Number', PrivilegeLevel as Privilege, DOB as DOB, uname as 'User Name'from Staffs");

                            if (pwd.Text != "")
                            {
                                db.updateTable("Update Staffs set pwd='" + pwd.Text + "' WHERE StaffID="+staffid);
                            }
                            DialogResult con= MessageBox.Show("Staff has been successfully edited! Would you like to edit another staff?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (con==DialogResult.Yes)
                            {
                                fname.Clear();
                                lname.Clear();
                                contactno.Clear();
                                Admin.Checked = false;
                                Assistant.Checked=false;
                                dob.ResetText();
                                uname.Clear();
                                pwd.Clear();
                                confirm.Clear();
                                pictureBox1.Image = initial;
                                pic = "";
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

        private void editStaffs_Load(object sender, EventArgs e)
        {
            initial = (Bitmap)pictureBox1.Image;
            dob.MaxDate = DateTime.Today.AddYears(-18);
            DB db = new DB();
            dataGridView1.DataSource = db.getTables("select StaffID  as ID, FirstName  as 'First Name', LastName as 'Last Name', ContactNo as 'Contact Number', PrivilegeLevel as Privilege, DOB as DOB, uname as 'User Name' from Staffs ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Regex rx = new Regex(@"[\d]+");
            if (custid.Text == "")
            {
                MessageBox.Show("Staff ID is empty!");
            }
            else if (!rx.IsMatch(custid.Text))
            {
                MessageBox.Show("Staff ID can only contain numbers!");
            }
            else
            {
                int id = Convert.ToInt32(custid.Text);
                DB db = new DB();
                dataGridView1.DataSource = db.getTables("select StaffID  as ID, FirstName  as 'First Name', LastName as 'Last Name', ContactNo as 'Contact Number', PrivilegeLevel as Privilege, DOB as DOB, uname as 'User Name'from Staffs WHERE StaffID="+id);
                //MessageBox.Show(dataGridView1.RowCount.ToString());
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("The staff for the entered ID is not found!");
                }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    staffid = Convert.ToInt32(item.Cells[0].Value);
                    fname.Text = item.Cells[1].Value.ToString();
                    lname.Text = item.Cells[2].Value.ToString();
                    contactno.Text = item.Cells[3].Value.ToString();
                    if (item.Cells[4].Value.ToString() == "Admin")
                    {
                        Admin.Checked = true;
                    }
                    else
                    {
                        Assistant.Checked = true;
                    }

                    dob.Value = Convert.ToDateTime(item.Cells[5].Value.ToString());
                    uname.Text = item.Cells[6].Value.ToString();
                    DB db = new DB();
                    foreach (DataRow r in db.getTables("select picture from Staffs WHERE StaffID=" + staffid).Rows)
                    {
                        if (r[0].ToString() != "" && r[0].ToString() != null)
                        {
                            pictureBox1.Image = new Bitmap(r[0].ToString());
                            pic = r[0].ToString();
                        }
                        else
                        {
                            pictureBox1.Image = initial;
                            pic = null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                        if (id == 1)
                        {
                            MessageBox.Show("Default admin cannot be removed!");
                        }
                        else
                        {
                            DB db = new ResMan.DB();
                            DialogResult res = MessageBox.Show("Are you sure that you want to remove?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.Yes)
                            {
                                db.updateTable("delete from Staffs where StaffID=" + id);
                                dataGridView1.DataSource = db.getTables("select StaffID  as ID, FirstName  as 'First Name', LastName as 'Last Name', ContactNo as 'Contact Number', PrivilegeLevel as Privilege, DOB as DOB, uname as 'User Name' from Staffs ");
                                MessageBox.Show("Staff removed!");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to delete since that would affect the already existing records.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Choose Profile Picture";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "JPEG FILES(*.jpg|*.jpg";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                pic = openFileDialog1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pic = null;
            pictureBox1.Image = initial;
        }

        private void custid_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = button2;
        }

        private void custid_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }
    }
}
