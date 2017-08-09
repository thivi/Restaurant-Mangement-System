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
    public partial class addStaffs : Form
    {
        Bitmap initial;
        public addStaffs()
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
            else if (pwd.Text == "")
            {
                MessageBox.Show("Password cannot be empty");
            }
            else if (pwd.Text.Length < 6)
            {
                MessageBox.Show("Password must contain more than 6 characters");
            }
            else if (!passA.IsMatch(pwd.Text) || !passa.IsMatch(pwd.Text) || !passd.IsMatch(pwd.Text) || !passS.IsMatch(pwd.Text))
            {
                MessageBox.Show("Password must contain atleast one simple lowercase alphabet, uppercase alphabet ,number and a special character!");
            }
            else if (pwd.Text != confirm.Text)
            {
                MessageBox.Show("Passwords you entered does not match each other!");
            }
            else
            {
                Boolean b;
                try
                {

                    DB db = new DB();
                    if (db.checkUname(uname.Text))
                    {
                        MessageBox.Show("Username alrady exists!", "Username already exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/FoodieSena");
                        string imgpath;
                        
                        if (openFileDialog1.FileName != "" && openFileDialog1.FileName!="openFileDialog1")
                        {
                            Bitmap img = new Bitmap(openFileDialog1.FileName);
                            imgpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/FoodieSena/" + uname.Text + ".jpg";
                            img.Save(imgpath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        else
                        {
                            imgpath = null;
                        }
                        b = db.addStaff(fname.Text, lname.Text, contactno.Text, dob.Value, privi, uname.Text, pwd.Text,imgpath);
                        if (b)
                        {
                            
                            DialogResult con= MessageBox.Show("Staff has been successfully added! Would you like to add another staff?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
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
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        else MessageBox.Show("An Error Occured!");
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                

            }
        }

        private void addStaffs_Load(object sender, EventArgs e)
        {
            dob.MaxDate = DateTime.Today.AddYears(-18);
            initial = (Bitmap)pictureBox1.Image;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Choose Profile Picture";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "JPEG FILES(*.jpg|*.jpg";
            openFileDialog1.ShowDialog();
            
            if (openFileDialog1.FileName!="")
                pictureBox1.Image = new Bitmap (openFileDialog1.FileName);
        }
    }
}
