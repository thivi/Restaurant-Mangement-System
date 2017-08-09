using ResMan.DataSet1TableAdapters;
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
    public partial class bill : Form
    {
        int oid;
        int cusid;
        int staffid;
        double disc;
        public bill(int oid, int cusid, int staffid, double disc)
        {
            this.oid = oid;
            this.cusid = cusid;
            this.staffid = staffid;
            this.disc = disc;
            InitializeComponent();
        }

        private void bill_Load(object sender, EventArgs e)
        {
            invoice iv = new invoice();
            DataSet1 ds = new DataSet1();
            DataTable1TableAdapter dt = new DataTable1TableAdapter();
            CustomersTableAdapter cu = new CustomersTableAdapter();
            StaffsTableAdapter st = new StaffsTableAdapter();
            dt.Fill(ds.DataTable1);
            cu.Fill(ds.Customers);
            st.Fill(ds.Staffs);
            iv.SetDataSource(ds);
            iv.SetParameterValue("orderid", oid);
            iv.SetParameterValue("custid", cusid);
            iv.SetParameterValue("staffid", staffid);
            iv.SetParameterValue("discount", disc);
            crystalReportViewer1.ReportSource = iv;
        }
    }
}
