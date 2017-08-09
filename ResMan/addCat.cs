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
    public partial class addCat : Form
    {
        public addCat()
        {
            InitializeComponent();
        }

        private void addc_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                if (db.checkCat(catname.Text))
                {
                    MessageBox.Show("Category alreday exists!", "Duplicate Content");
                }
                else if (catname.Text == "")
                {
                    MessageBox.Show("Catgeory Name cannot be empty");
                }
                else
                {
                    if (db.addCategory(catname.Text))
                    {
                        DialogResult res = MessageBox.Show("Category added successfully! Would you like to add another?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (res == DialogResult.Yes)
                        {
                            catname.Clear();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("An error occured!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
