using System;
using System.Collections;
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
    public partial class Order : Form
    {
        int oid;
        int staffid;
        int custid = -1;
        ArrayList catlist;
        ArrayList foodlist;
        ArrayList foodidlist=new ArrayList();
        DB db;
        double price;
        double disc=0;
        double discount=0;
        double amnt=0;
        int catitemid;
        DataTable dt = new DataTable();
        DataColumn cl;
        DataRow rw;
        public Order(int sid)
        {
            db = new DB();
            staffid = sid;
            catlist = db.getCat();

            cl = new DataColumn();
            cl.ColumnName = "ID";
            cl.DataType = System.Type.GetType("System.Int32");
            dt.Columns.Add(cl);
            cl = new DataColumn();
            cl.ColumnName = "Category";
            dt.Columns.Add(cl);
            cl = new DataColumn();
            cl.ColumnName = "Item";
            dt.Columns.Add(cl);
            cl = new DataColumn();
            cl.ColumnName = "Qty";
            dt.Columns.Add(cl);
            cl = new DataColumn();
            cl.ColumnName = "Unit Price";
            dt.Columns.Add(cl);
            cl = new DataColumn();
            cl.ColumnName = "Total";
            cl.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(cl);


            InitializeComponent();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            
            foreach (catitem ct in catlist)
            {
                listBox1.Items.Add(ct.catname);
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Regex r = new Regex(@"[\d]{9}[Vv]");
            

            if (nic.Text == "")
            {
                MessageBox.Show("NIC Number cannot be empty!");
            }
            else if (!r.IsMatch(nic.Text))
            {
                MessageBox.Show("NIC number is invalid!");
            }
            else
            {
                if (db.getCustID(nic.Text) == -1)
                {
                    MessageBox.Show("NIC number not found!");

                }
                else
                {
                    custid = db.getCustID(nic.Text);
                    
                    label6.Text = db.getCustName(custid);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < (listBox1.Items.Count))
                {
                

                try
                {
                    catitem ct = (catitem)catlist[listBox1.SelectedIndex];
                    foodlist = db.getfoodId(ct.catid);

                    listBox2.Items.Clear();
                    foodidlist.Clear();
                    foreach (string foodname in foodlist)
                    {

                        foodidlist.Add(db.getFoodId(foodname).ToString());
                        listBox2.Items.Add(foodname);
                    }
                }
                catch (ArgumentOutOfRangeException exa)
                {
                    MessageBox.Show("Choose the right category!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < (listBox2.Items.Count))
            {
                try
                {
                    catitem ct = (catitem)catlist[listBox1.SelectedIndex];
                    int catid = ct.catid;
                    int foodid = Convert.ToInt32(foodidlist[listBox2.SelectedIndex]);

                    price = db.getPrice(catid, foodid);
                    catitemid = db.getCatItemID(catid, foodid);
                    label7.Text = price.ToString();
                }
                catch (ArgumentOutOfRangeException exa)
                {
                    MessageBox.Show("Choose the right Item!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rx = new Regex(@"[\d]+");
            Boolean cat = false;
            foreach(DataRow r in dt.Rows)
            {
               if(Convert.ToInt32(r["ID"]) == catitemid)
                {
                    cat = true;
                }
            }
            
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Pleases select a Category!");

            }
            else if (listBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a Food Item!");
            }
            else if (qty.Text == "")
            {
                MessageBox.Show("Specify a qunatity!");
            }
            else if (!rx.IsMatch(qty.Text))
            {
                MessageBox.Show("Quantity may only contain integers!");
            }
            else if (cat)
            {
                MessageBox.Show("Item has already been added. If you want adjust the qty, rmove the existing item and add again!");
            }
            else
            {
                catitem ct = (catitem)catlist[listBox1.SelectedIndex];
                rw = dt.NewRow();
                rw["ID"] = catitemid;
                rw["Category"] = ct.catname;
                rw["Item"] = foodlist[listBox2.SelectedIndex];
                rw["Qty"] = qty.Text;
                rw["Unit Price"] = price;
                rw["Total"] = price * Convert.ToDouble(qty.Text);
                dt.Rows.Add(rw);
                dataGridView1.DataSource = dt;
                object sum=dt.Compute("Sum(Total)","");
                amnt = Convert.ToDouble(sum);
                label9.Text = amnt.ToString();
                disc = amnt;
                if (amnt > 1000 && DateTime.Today.DayOfWeek==DayOfWeek.Thursday && custid != -1)
                {
                    disc =((90 / 100.0) * amnt);
                    discount = (10 / 100.0) * amnt;
                    
                }
                label12.Text = disc.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count==0)
            {
                MessageBox.Show("Please select a row from the table");

            }
            else 
            {           
                foreach(DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    dt.Rows.RemoveAt(item.Index);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
                object sum = dt.Compute("Sum(Total)", "");
                if (!(sum is DBNull))
                {
                    label9.Text = sum.ToString();
                    amnt = Convert.ToDouble(sum);
                    disc = amnt;

                    if (amnt > 1000 && DateTime.Today.DayOfWeek == DayOfWeek.Thursday && custid!=-1)
                    {
                        disc = ((90 / 100.0) * amnt);
                        discount = (10 / 100.0) * amnt;

                    }
                    label12.Text = disc.ToString();
                }
                else
                {
                    amnt = 0;
                    disc = 0;
                    label9.Text = amnt.ToString();
                    label12.Text = disc.ToString();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (custid == -1)
            {
                DialogResult dr = MessageBox.Show("A Customer has not been chosen. Do you like to continue?", "No Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (disc == 0 || dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("Please place orders before confirming payment!");
                    }
                    else
                    {
                        try
                        {
                            db.addOrder(1, staffid, DateTime.Today);
                            try
                            {
                                oid = db.getLastID();
                                
                                foreach (DataRow r in dt.Rows)
                                {

                                    db.addOrderItem(oid, Convert.ToInt32(r["ID"]), Convert.ToInt32(r["Qty"]));
                                }
                                //MessageBox.Show(oid.ToString() + " " + custid.ToString() + " " + staffid.ToString() + " " + discount.ToString());
                                bill b = new bill(oid, 1, staffid, discount);
                                b.Show();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                else if (dr == DialogResult.No)
                {

                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Do you want to complete the order?", "Complete Order", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {


                    if (disc == 0 || dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("Please place orders before confirming payment!");
                    }
                    else
                    {
                        try
                        {
                            db.addOrder(custid, staffid, DateTime.Today);
                            try
                            {
                                oid = db.getLastID();

                                foreach (DataRow r in dt.Rows)
                                {

                                    db.addOrderItem(oid, Convert.ToInt32(r["ID"]), Convert.ToInt32(r["Qty"]));
                                }

                                bill b = new bill(oid, custid, staffid, discount);
                                b.Show();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }


                    }
                }
            }
        }

        private void nic_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = button3;
        }

        private void nic_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = button4;
        }
    }
}
