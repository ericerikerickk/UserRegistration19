using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace UserRegistration19
{
    public partial class UserInfoForm : Form
    {
        DateTime myDateTime = DateTime.Now;
        private string username;
        public UserInfoForm(string username)
        {
            InitializeComponent();
            this.username = username;
            displayData();
            if (txtID.Text == "" || txtFname.Text == "" || txtLname.Text == "" || txtContact.Text == "" || txtAddress.Text == "")
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }

        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand checkID = new SqlCommand("select Idnum from users where Idnum='" + int.Parse(txtID.Text) + "'", con);
            SqlCommand checkFname = new SqlCommand("select fname from users where fname='" + txtFname.Text + "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(checkID);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            SqlDataAdapter sdFname = new SqlDataAdapter(checkFname);
            DataTable dtFname = new DataTable();
            sdFname.Fill(dtFname);
            // Checking Last Name
            SqlCommand checkLname = new SqlCommand("select lname from users where lname='" + txtLname.Text + "'", con);
            SqlDataAdapter sdLname = new SqlDataAdapter(checkLname);
            DataTable dtLname = new DataTable();
            sdLname.Fill(dtLname);
            // Checking Contact
            SqlCommand checkContact = new SqlCommand("select contact from users where contact='" + long.Parse(txtContact.Text) + "'", con);
            SqlDataAdapter sdContact = new SqlDataAdapter(checkContact);
            DataTable dtContact = new DataTable();
            sdContact.Fill(dtContact);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("ID already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else if (dtFname.Rows.Count > 0)
            {
                MessageBox.Show("First Name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else if (dtLname.Rows.Count > 0)
            {
                MessageBox.Show("Last Name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else if (dtContact.Rows.Count > 0)
            {
                MessageBox.Show("Contact Number already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                long contact = long.Parse(txtContact.Text);
                SqlCommand cmd = new SqlCommand("Insert into users values ('" + txtID.Text + "', '" + txtFname.Text + "', '" + txtLname.Text + "', '" + txtAddress.Text + "', '" + contact + "' where UserName = '" + username + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Saved!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                displayData();
            }
            
        }
            /* Checking Last Name
            SqlCommand checkLname = new SqlCommand("select lname from users where UserName!='" + username + "'", con);
            SqlDataAdapter sdLname = new SqlDataAdapter(checkLname);
            DataTable dtLname = new DataTable();
            sdLname.Fill(dtLname);
            // Checking Contact
            SqlCommand checkContact = new SqlCommand("select contact from users where UserName!='" + username + "'", con);
            SqlDataAdapter sdContact = new SqlDataAdapter(checkContact);
            DataTable dtContact = new DataTable();
            sdContact.Fill(dtContact);
            
            /*else if (dtFname.Rows.Count > 0)
            {
                MessageBox.Show("First Name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else if (dtLname.Rows.Count > 0)
            {
                MessageBox.Show("Last Name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else if (dtContact.Rows.Count > 0)
            {
                MessageBox.Show("Contact Number already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                SqlCommand cmd = new SqlCommand()
            }
            */
        private void displayData()
        {
            con.Open();
            SqlCommand idCmd = new SqlCommand("select Idnum from users where UserName='" + username + "'", con);
            String num = Convert.ToString(idCmd.ExecuteScalar());
            txtID.Text = num;
            con.Close();
            con.Open();
            SqlCommand fnameCmd = new SqlCommand("select fname from users where UserName='" + username + "'", con);
            txtFname.Text = Convert.ToString(fnameCmd.ExecuteScalar());
            con.Close();
            con.Open();
            SqlCommand lnameCmd = new SqlCommand("select lname from users where UserName='" + username + "'", con);
            txtLname.Text = Convert.ToString(lnameCmd.ExecuteScalar());
            con.Close();
            con.Open();
            SqlCommand addressCmd = new SqlCommand("select address from users where UserName='" + username + "'", con);
            txtAddress.Text = Convert.ToString(addressCmd.ExecuteScalar());
            con.Close();
            con.Open();
            SqlCommand contactCmd = new SqlCommand("select contact from users where UserName='" + username + "'", con);
            txtContact.Text = Convert.ToString(contactCmd.ExecuteScalar());
            con.Close();
        }

        private void btnID_Click(object sender, EventArgs e)
        {
            con.Open();
            //Checking ID number
            SqlCommand checkID = new SqlCommand("select * from users where Idnum='" + int.Parse(txtID.Text) + "'", con);
            SqlDataAdapter sdID = new SqlDataAdapter(checkID);
            DataTable dtID = new DataTable();
            sdID.Fill(dtID);
            con.Close();
            // Checking same value
            con.Open();
            SqlCommand check1 = new SqlCommand("select Idnum from users where UserName='" + username + "'", con);
            string checkLastname = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkLastname == txtID.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (dtID.Rows.Count > 0)
            {
                MessageBox.Show("ID number already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET Idnum='" + int.Parse(txtID.Text) + "' where UserName = '" + username + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("ID number Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnFname_Click(object sender, EventArgs e)
        {
            con.Open();
            //Checking First Name 
            SqlCommand checkFname = new SqlCommand("select * from users where fname='" + txtFname.Text + "'", con);
            SqlDataAdapter sdFame = new SqlDataAdapter(checkFname);
            DataTable dtFname = new DataTable();
            sdFame.Fill(dtFname);
            con.Close();
            // Checking same value
            con.Open();
            SqlCommand check1 = new SqlCommand("select fname from users where UserName='" + username + "'", con);
            string checkFirstname = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkFirstname == txtFname.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (dtFname.Rows.Count > 0)
            {
                MessageBox.Show("First name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET fname='" + txtFname.Text + "' where UserName = '" + username + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("First Name Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnLname_Click(object sender, EventArgs e)
        {
            con.Open();
            //Checking Last name 
            SqlCommand checkLname = new SqlCommand("select * from users where lname='" + txtLname.Text + "'", con);
            SqlDataAdapter sdLame = new SqlDataAdapter(checkLname);
            DataTable dtLname = new DataTable();
            sdLame.Fill(dtLname);
            con.Close();
            // Checking same value
            con.Open();
            SqlCommand check1 = new SqlCommand("select lname from users where UserName='" + username + "'", con);
            string checkLastname = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkLastname == txtLname.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if(dtLname.Rows.Count > 0)
            {
                MessageBox.Show("Last name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET lname='" + txtLname.Text + "' where UserName = '" + username + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Last Name Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnAddress_Click(object sender, EventArgs e)
        {
            con.Open();
            // Checking same value
            SqlCommand check1 = new SqlCommand("select address from users where UserName='" + username + "'", con);
            string checkAddresss = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkAddresss == txtAddress.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET address='" + txtAddress.Text + "' where UserName = '" + username + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Address Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            con.Open();
            // Checking same value
            SqlCommand checkContact = new SqlCommand("select contact from users where UserName='" + username + "'", con);
            string check = Convert.ToString(checkContact.ExecuteScalar());
            con.Close();
            if (check == txtAddress.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET contact='" + long.Parse(txtContact.Text) + "' where UserName = '" + username + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Contact Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }
    }
}
    
  

