using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResMan
{

    public partial class dashboard : Form
    {
        string uname;
        int staffID;
        public dashboard(string username)
        {
            uname = username;
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addStaffs staff= new addStaffs();
            staff.Show();
        }

        private void addCustomer_Click(object sender, EventArgs e)
        {
            addCustomer cust = new addCustomer();
            cust.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addCat ac = new addCat();
            ac.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            addFood af = new addFood();
            af.Show();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            
            DB db = new DB();
            
            if (db.getStaffID(uname) == -1)
            {
                MessageBox.Show("Login Error!");
                Application.Exit();
            }
            else
            {
                staffID = db.getStaffID(uname);
            }
            DataTable dt=db.getTables("select PrivilegeLevel from Staffs where StaffID=" + staffID);
            
            foreach (DataRow r in dt.Rows)
            {
                if (r[0].ToString() == "Assistant")
                {
                    addStaff.Enabled = false;
                    button4.Enabled = false;
                }
                label2.Text = r[0].ToString();
            }
            DataTable sname = db.getTables2("select FirstName, LastName, picture from Staffs WHERE StaffID=" + staffID);
            foreach (DataRow row in sname.Rows)
            {
                label1.Text = row[0].ToString()+" "+row[1].ToString();
                
                if (row[2].ToString() != "" && row[2].ToString()!=null)
                {
                    pictureBox2.Image = new Bitmap(row[2].ToString());
                }
                    
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Order o = new Order(staffID);
            
            o.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            editStaffs es = new editStaffs();
            es.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            editCustomer ec = new editCustomer();
            ec.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            editCat ec = new editCat();
            ec.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            editFood ef = new editFood();
            ef.Show();
        }

        private void dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
