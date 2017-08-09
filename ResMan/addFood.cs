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
    public partial class addFood : Form
    {
        DB db;
        ArrayList list;
        int y = 0;
        public addFood()
        {
            InitializeComponent();
            db = new DB();
            list = db.getCat();
        }

        private void addFood_Load(object sender, EventArgs e)
        {
           
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
                    if (db.checkFood(foodname.Text))
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
                                
                                Boolean result = true; ;
                                if (db.addFood(foodname.Text))
                                {
                                    int foodid = db.getFoodId(foodname.Text);
                                    if (foodid == -1)
                                    {
                                        MessageBox.Show("Unable to find the corresponding ID of the Food Name entered!");
                                    }
                                    else
                                    {
                                        foreach (catitem ct in sellist)
                                        {
                                            if (db.addCatFood(ct.catid, foodid, ct.uprice))
                                            {
                                                result = true;
                                            }
                                            else
                                            {
                                                MessageBox.Show("An Error Occured while adding members to CategoriesItems table!");
                                                result = false;
                                                break;
                                            }
                                        }
                                        if (result != false)
                                        {
                                            DialogResult res = MessageBox.Show("Food successfully added! Would you like to add another?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                                            if (res == DialogResult.Yes)
                                            {
                                                this.Close();
                                                addFood af = new addFood();
                                                af.Show();

                                            }
                                            else
                                            {
                                                this.Close();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("An error occured while adding members to Items table!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }


                        /*Unit_Price up=new Unit_Price(sellist, foodname.Text);
                        up.Show();
                        this.Close();*/
                    }
                }
                catch(FormatException fe)
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
    }
}
