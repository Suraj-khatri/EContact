using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EContact.EcontactFunctionality
{
    public class EcontactClasses
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        static string myConn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        //selecting data from database
        public DataTable Select()
        {
            //step1: Database connection
            SqlConnection conn = new SqlConnection(myConn);
            DataTable dt = new DataTable();
            try
            {
                //step 2: writing sql query
                string sql = "SELECT * FROM tbl_contact";

                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public bool Insert(EcontactClasses c)
        {
            bool IsSucess = false;
            SqlConnection conn = new SqlConnection(myConn);
            try
            {
                string sql = "INSERT INTO tbl_contact(FirstName,LastName,ContactNo,Address,Gender) VALUES(@FirstName,@LastName,@ContactNo,@Address,@Gender)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    IsSucess = true;
                }
                else
                {
                    IsSucess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally { conn.Close(); }
            return IsSucess;
        }

        public bool Update(EcontactClasses c)
        {
            bool IsSucess = false;
            SqlConnection conn = new SqlConnection(myConn);
            try
            {
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName,LastName=@LastName,ContactNo=@ContactNo,Address=@Address,Gender=@Gender WHERE ContactID = @ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                conn.Open();

                int row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    IsSucess = true;
                }
                else
                {
                    IsSucess = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally { conn.Close(); }
            return IsSucess;
        }

        public bool Delete(EcontactClasses c)
        {
            bool IsSucess = false;
            SqlConnection conn = new SqlConnection(myConn);
            try
            {
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand (sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                conn.Open();
                int row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    IsSucess = true;
                    
                }
                else
                {
                    IsSucess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally { conn.Close(); }
            return IsSucess;
        }

        public void Search(string c)
        {
            
            
            
        }
    }
}
