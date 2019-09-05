using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    public class contactClass
    {
        //Getter Setter Properties
        //Acts as Data carrier in our application

        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrg"].ConnectionString;

        //Selecting Data From Database

        public DataTable Select()
        {
            ///Step 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2: Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL Data Adapter using cmd
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

        //Inserting Data into Database
        public bool Insert(contactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            //Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Step 2: Create SQL Query to Insert Data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES(@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //Creating SQL Command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating Parameters to add Data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If run successfully value of rows will be greater than zero else value will be 0.
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to update database from our application

        public bool Update(contactClass c)
        {
            //Create a default return type and set its value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to Update data in our database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to Add Value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open Database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if query runs successfully value of rows will be greater than zero else value will be zero
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to delete data from database

        public bool Delete(contactClass c)
        {
            //Create a default return value and set value to false
            bool isSuccess = false;
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to Delete Data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if rows is greater than zero it was successfull else it value will be zero
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
