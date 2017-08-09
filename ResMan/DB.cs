using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResMan
{
    class DB
    {
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        

        internal DB()
        {
            conn.ConnectionString = "Server=Localhost; Database=ResMan; Integrated Security=true";
            conn.Open();

  
        }

        internal Boolean login(string uname, string pwd)
        {

            da = new SqlDataAdapter("Select * from Staffs WHERE uname='"+uname+"' AND pwd='"+pwd+"'",conn);
            da.Fill(ds,"UserLogin");

            DataTable dt = ds.Tables["UserLogin"];
            DataRow[] rows = dt.Select();

            if (rows.Length == 1)
            {
                return true;
            }
            return false;

        }

       internal Boolean addStaff(string fname, string lname, string cno, DateTime dob, string privilege, string un, string pwd, string imgpath)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Staffs (FirstName, LastName, ContactNo, PrivilegeLevel, DOB, uname, pwd, picture) values('" + fname + "','" + lname + "','" + cno + "','" + privilege + "','" + dob + "','" + un + "','" + pwd + "','"+imgpath+"')";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal Boolean checkUname(string uname)
        {
            da = new SqlDataAdapter("Select * from Staffs WHERE uname='" + uname +"'", conn);
            da.Fill(ds, "Uname");
            DataTable udt = ds.Tables["Uname"];
            DataRow[] udr = udt.Select();
            if (udr.Length !=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal Boolean addCustomer(string fname, string lname, string cno, string nic, DateTime dob)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Customers (FirstName, LastName, ContactNo, NICNo, DOB, RegDate) values ('" + fname + "','" + lname + "','" + cno + "','" + nic + "','" + dob + "','" + DateTime.Today + "')";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal Boolean checkNIC(string nic)
        {

            da = new SqlDataAdapter("select * from Customers WHERE NICNo='" + nic + "'", conn);
            da.Fill(ds,"NIC");
            DataTable dt = ds.Tables["NIC"];
            DataRow[] dr = dt.Select();
            if (dr.Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal Boolean addCategory(string catname)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Categories (CatName) values('" + catname + "')";
                cmd.Connection = conn;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal Boolean checkCat(string catname)
        {
            
                da = new SqlDataAdapter("select * from Categories WHERE CatName = '" + catname + "'", conn);
                da.Fill(ds, "checkCatName");

                DataTable dt = ds.Tables["checkCatName"];
                DataRow[] dr = dt.Select();

                if (dr.Length != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
              
            
        }

        internal ArrayList getCat()
        {
            da = new SqlDataAdapter("select CatID, CatName from Categories", conn);
            da.Fill(ds, "Categories");
            DataTable dt = ds.Tables["Categories"];
            ArrayList list = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                

                list.Add(new catitem { catid = Convert.ToInt32(dr[0].ToString()), catname = dr[1].ToString() });
            }
            return list;
        }

        internal Boolean addFood(string food)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Items (ItemName) values('" + food + "')";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal Boolean checkFood(string food)
        {
            da = new SqlDataAdapter("select * from Items WHERE ItemName='" + food + "'", conn);
            da.Fill(ds, "checkFood");
            DataTable dt = ds.Tables["checkFood"];
            DataRow[] dr = dt.Select();
            if (dr.Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal Boolean addCatFood(int catid, int foodid, double up)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into CategoriesItems (CatID, ItemID, UnitPrice) values('" + catid + "','" + foodid + "'," + up + ")";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        internal int getFoodId(string foodname)
        {
            da = new SqlDataAdapter("select ItemID from Items WHERE ItemName='" + foodname + "'", conn);
            da.Fill(ds, "FoodID");
            ds.Tables["FoodID"].Clear();
            da.Fill(ds, "FoodID");
            DataRow[] dr = ds.Tables["FoodID"].Select();

            if (dr.Length == 0)
            {
                Console.WriteLine("Error!");
                return -1;
            }
            else
            {
                Console.WriteLine(dr[0][0].ToString());
                return Convert.ToInt32(dr[0][0]);
            }
        }

        internal int getStaffID(string uname)
        {
            da = new SqlDataAdapter("select StaffID from Staffs WHERE uname='" + uname + "'", conn);
            da.Fill(ds, "StaffID");
            DataRow[] rows = ds.Tables["StaffID"].Select();
            if (rows.Length != 0)
            {
                return Convert.ToInt32(rows[0][0]);
            }
            else
            {
                return -1;
            }
        }

        internal int getCustID(string nic)
        {
            da = new SqlDataAdapter("select CustomerID from Customers WHERE NICno='" + nic + "'", conn);
            da.Fill(ds, "CusID");
            DataRow[] rows = ds.Tables["CusID"].Select();
            if (rows.Length != 0)
            {
                return Convert.ToInt32(rows[0][0]);
            }
            else
            {
                return -1;
            }
        }
        internal string getCustName(int c)
        {
            try
            {
                da = new SqlDataAdapter("select FirstName, LastName from Customers WHERE CustomerID="+c, conn);
                da.Fill(ds, "CusName");
                DataRow[] rows = ds.Tables["CusName"].Select();
                if (rows.Length == 0)
                {
                    return "Error!";
                }
                else
                {
                    return rows[0][0].ToString() + " " + rows[0][1];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Error!";
            }
        }

        internal ArrayList getfoodId(int catid)
        {
            ArrayList list = new ArrayList();
            try
            {
                
                da = new SqlDataAdapter("select ItemName from CategoriesItems c, Items i WHERE c.CatID="+catid+" AND c.ItemID=i.ItemID", conn);
                
                da.Fill(ds, "FoodName");
                ds.Tables["FoodName"].Clear();
                da.Fill(ds, "FoodName");
                DataRow[] rows = ds.Tables["FoodName"].Select();
                if (rows.Length == 0)
                {
                    //list.Add("-1");
                    return list;
                }
                else
                {
                    foreach (DataRow dr in ds.Tables["FoodName"].Rows)
                    {
                        list.Add(dr[0].ToString());
                        //Console.WriteLine(dr[0].ToString());
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                list.Add("-1");
                return list;
            }

            
        }

        internal double getPrice(int catid, int foodid)
        {
            da = new SqlDataAdapter("select UnitPrice from CategoriesItems WHERE CatID='" + catid + "' AND ItemID='" + foodid + "'", conn);
            da.Fill(ds, "Price");
            ds.Tables["Price"].Clear();
            da.Fill(ds, "Price");
            DataRow[] rows = ds.Tables["Price"].Select();

            if (rows.Length == 0)
            {
                return -1;
            }
            else {
                return Convert.ToDouble(rows[0][0]);
            }
        }

        internal int getCatItemID(int catid, int foodid)
        {
            da = new SqlDataAdapter("select CatItemID from CategoriesItems WHERE CatID='" + catid + "' AND ItemID='" + foodid + "'", conn);
            da.Fill(ds, "catitem");
            ds.Tables["catitem"].Clear();
            da.Fill(ds, "catitem");
            DataRow[] rows = ds.Tables["catitem"].Select();

            if (rows.Length == 0)
            {
                return -1;
            }
            else
            {
                return Convert.ToInt32(rows[0][0]);
            }
        }

        internal void addOrder(int custid, int staffid, DateTime date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText="insert into Orders (CustID, StaffID, OrderDate) values("+custid+","+staffid+",'"+date+"')";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal void addOrderItem(int orderid, int catitemid, int qty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into OrderedItems values(" + orderid + "," + catitemid + "," + qty + ")";
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal int getLastID()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                cmd.Connection = conn;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable tab = new DataTable();
                tab.Load(dr);
                
                return Convert.ToInt32(tab.Rows[0][0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        internal DataTable getTables(string table)
        {
            int i = 0;
            string tab= "CustomTable" + i.ToString();
            da = new SqlDataAdapter(table, conn);
            da.Fill(ds, tab);
            ds.Tables[tab].Clear();
            da.Fill(ds, tab);
            i++;
            return ds.Tables[tab];

        }
        internal DataTable getTables2(string table)
        {
            da = new SqlDataAdapter(table, conn);
            da.Fill(ds, "CustomTable1");
            ds.Tables["CustomTable1"].Clear();
            da.Fill(ds, "CustomTable1");

            return ds.Tables["CustomTable1"];

        }

        internal void updateTable(string table)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = table;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }
    }
}
