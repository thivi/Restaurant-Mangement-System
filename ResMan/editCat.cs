using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResMan
{
    public partial class editCat : Form
    {
        int catid = -1;
        public editCat()
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
                else if (catid == -1)
                {
                    MessageBox.Show("Category not set!");
                }
                else
                {

                    db.updateTable("update Categories set CatName='" + catname.Text + "' WHERE CatID=" + catid);
                    dataGridView1.DataSource = db.getTables("select CatID as 'ID', CatName as 'Name' from Categories");

                    DialogResult res = MessageBox.Show("Category edited successfully! Would you like to edit another?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (res == DialogResult.Yes)
                        {
                            catname.Clear();
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

        private void editCat_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            dataGridView1.DataSource = db.getTables("select CatID as 'ID', CatName as 'Name' from Categories");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("Customer ID is empty!");
            }
            
            else
            {
                string id = textBox1.Text;
                DB db = new DB();
                dataGridView1.DataSource = db.getTables("select CatID as 'ID', CatName as 'Name' from Categories WHERE CatName='" + id+"'");
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("The category for the entered name is not found!");
                }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                foreach(DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    catid = Convert.ToInt32(item.Cells[0].Value);
                    catname.Text = item.Cells[1].Value.ToString();
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
                            db.updateTable("delete from Categories where CatID=" + id);
                            dataGridView1.DataSource = db.getTables("select CatID as 'ID', CatName as 'Name' from Categories");
                            MessageBox.Show("Category removed!");
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
            this.AcceptButton = addc;
        }
    }
}
