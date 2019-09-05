using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting Data into Database
            bool success = c.Insert(c);
            if (success)
            {
                //Successfully Inserted
                MessageBox.Show("New Contact Successfully Inserted");
                Clear();
            }
            else
            {
                //Failed to add contact.
                MessageBox.Show("Failed to add contact. Try again");
            }
            //Load Data on Data Grid View
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void txtboxContactID_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get Data From Text Boxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Update Data in Database
            bool success = c.Update(c);
            if (success)
            {
                //Update Successfully
                MessageBox.Show("Contact has been updated successfully");
                //Load Data on Data Grid
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                //Update Failed
                MessageBox.Show("Failed to Update. Try again");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get Data from Data Grid View and Load into the textboxes respectively
            //Identify row mouse was clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call Clear Method Here
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if(success)
            {
                //Successfully Deleted contact
                MessageBox.Show("Contact Successfully Deleted.");
                //Refresh Data Grid View
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact. Try Again");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrg"].ConnectionString;

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from text box
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
