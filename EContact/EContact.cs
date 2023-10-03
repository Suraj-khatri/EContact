using EContact.EcontactFunctionality;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace EContact
{
    public partial class EContact : Telerik.WinControls.UI.RadForm
    {
        public EContact()
        {
            InitializeComponent();
        }

        EcontactClasses c = new EcontactClasses();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Insert(c);
            if (success)
            {
                MessageBox.Show("Inserted successfully");
                CLear();

            }
            else
            {
                MessageBox.Show("Insert failed");
            }
            //load data on grisview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;


        }

        private void EContact_Load(object sender, EventArgs e)
        {
            //load data on grisview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void CLear()
        {
            txtboxContactId.Text = "";
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNo.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from grid view and load to text boxes
            int rowIndex = e.RowIndex;
            txtboxContactId.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(txtboxContactId.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Upated Sucessfully");
                //load data on grisview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                CLear();
            }
            else
            {
                MessageBox.Show("Failed to update .Try again");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtboxContactId.Text);
            

            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Deleted Successfully");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                CLear();
            }
            else
            {
                MessageBox.Show("Failed to Delete. Try Again");
            }
        }

        static string myConn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private void btnClear_Click(object sender, EventArgs e)
        {
           
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string c = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myConn);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + c + "%' OR LastName LIKE '%" + c + "%' OR Address LIKE '%" + c + "%'", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
