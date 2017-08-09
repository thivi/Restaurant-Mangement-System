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
    public partial class editFood : Form
    {
        DB db;
        ArrayList list;
        int y = 0;
        int itemid;
        int catitemid;
        DataTable data;
        public editFood()
        {
            InitializeComponent();
            db = new DB();
            list = db.getCat();
        }

        private void editFood_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.getTables("select ItemID as 'ID', ItemName as 'Name' from Items");
            foreach (catitem ct in list)
            {
                listBox1.Items.Add(ct.catname);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ArrayList sellist = new ArrayList();
            if (foodname.Text == "")
            {
                MessageBox.Show("Food Name cannot be empty!");
            }
            else if (listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select atleast one category!");
            }
            else
            {
                try
                {
                    DB db = new DB();
                    if (db.checkFood(foodname.Text) && db.getFoodId(foodname.Text)!=itemid)
                    {
                        MessageBox.Show("Food Name alrady exists!", "Duplicate Content!");
                    }
                    else
                    {
                        Double upr;
                        ArrayList sell = new ArrayList();
                        Regex digi = new Regex(@"[\d\.]+");
                        Boolean result1 = true;
                        foreach (string n in listBox1.SelectedItems)
                        {


                            if (panel1.Controls.Find(n, true)[0].Text == "")
                            {
                                MessageBox.Show("Unit Price cannot be empty!");
                                result1 = false;
                            }
                            else if (!digi.IsMatch(panel1.Controls.Find(n, true)[0].Text))
                            {
                                MessageBox.Show("Number format is incorrect!");
                                result1 = false;
                            }
                            else
                            {
                                upr = Convert.ToDouble(this.Controls.Find(n, true)[0].Text);
                                foreach (catitem ct in list)
                                {
                                    if (n.Equals(ct.catname))
                                    {
                                        sellist.Add(new catitem { catid = ct.catid, catname = ct.catname, uprice = upr });
                                    }
                                }
                                
                                
                                result1 = true;
                            }
                        }

                        try
                        {
                            if (result1 == true)
                            {
                                
                                
                                db.updateTable("update Items set ItemName='" + foodname.Text + "' WHERE ItemID=" + itemid);
                                    int foodid = db.getFoodId(foodname.Text);
                                    if (foodid == -1)
                                    {
                                        MessageBox.Show("Unable to find the corresponding ID of the Food Name entered!");
                                    }
                                    else
                                    {
                                        foreach (catitem ct in sellist)
                                        {
                                        int addid=-1;
                                        bool exist = false;
                                        
                                            foreach (DataRow item in data.Rows)
                                            {
                                            
                                                foreach (catitem c in list)
                                                {
                                                    if (item[1].ToString() == c.catname)
                                                    {
                                                        addid = c.catid;

                                                    }
                                                }

                                                if (addid == ct.catid)
                                                {
                                                    exist = true;
                                                }
                                                

                                            }

                                        if (!exist)
                                        {
                                            db.updateTable("insert into CategoriesItems values(" + ct.catid + "," + foodid + "," + ct.uprice + ")");
                                        }
                                        else
                                        {
                                            db.updateTable("update CategoriesItems set UnitPrice=" + ct.uprice + "WHERE CatID=" + ct.catid + " AND ItemID=" + foodid);
                                        }

                                        }
                                        
                                            DialogResult res = MessageBox.Show("Food successfully edited! Would you like to edit another?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                                            if (res == DialogResult.Yes)
                                            {
                                                this.Close();
                                                editFood af = new editFood();
                                                af.Show();

                                            }
                                            else
                                            {
                                                this.Close();
                                            }
                                        
                                    }
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }


                        
                    }
                }
                catch (FormatException fe)
                {
                    MessageBox.Show("Number format is incorrect!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList cl = new ArrayList();
            foreach (string lb in listBox1.SelectedItems)
            {
                
                foreach (Control c in panel1.Controls)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        if (c.Name == lb)
                        {
                            cl.Add(new controli { name = c.Name, text = c.Text });
                        }
                        
                    }
                }

                
            }
            panel1.Controls.Clear();
            y = 0;
            
            foreach(string lb in listBox1.SelectedItems)
            {
                
                    Label lab = new Label();
                    lab.Text = lb;
                    lab.Location = new Point(0, y);

                    panel1.Controls.Add(lab);

                    TextBox text = new TextBox();
                    text.Name = lb;
                    foreach (controli co in cl)
                {
                    if (co.name == lb)
                    {
                        text.Text = co.text;
                    }
                }
                    text.Location = new Point(150, y);
                    panel1.Controls.Add(text);
                    y += 30;
                
                
                
            }
            


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Food Item ID is empty!");
            }

            else
            {
                string id = textBox1.Text;
                DB db = new DB();
                dataGridView1.DataSource = db.getTables("select ItemID as 'ID', ItemName as 'Name' from Items WHERE ItemName='" + id + "'");
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("The food item for the entered name is not found!");
                }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                listBox1.ClearSelected();
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    itemid = Convert.ToInt32(item.Cells[0].Value);
                    foodname.Text= item.Cells[1].Value.ToString();
                }
                data = db.getTables2("select ci.CatItemID as 'ID', c.CatName as 'Category', ci.UnitPrice as 'Unit Price' from CategoriesItems ci, Categories c where c.CatID=ci.CatID AND ci.ItemID=" + itemid);
                dataGridView2.DataSource = data;
                foreach (DataRow rw in data.Rows)
                {
                    listBox1.SelectedItem = rw[1].ToString();
                    foreach (Control c in panel1.Controls)
                    {
                        if (c.Name == rw[1].ToString())
                        {
                            c.Text = rw[2].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = button2;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }
    }
}
