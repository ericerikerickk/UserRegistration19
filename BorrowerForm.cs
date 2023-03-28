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
    public partial class BorrowerForm : Form
    {
        public BorrowerForm()
        {
            InitializeComponent();
            loadDataGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("Select Idnum AS [ID],  fname AS [First Name], lname AS [Last Name], address AS [Address], contact AS [Contact Number], UserName AS [Username], password AS [Password] from users where UserName != 'admin' order by Id asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }

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
            // Checking Username
            SqlCommand checkUser = new SqlCommand("select UserName from users where UserName='" + txtUserName.Text + "'", con);
            SqlDataAdapter sdUser = new SqlDataAdapter(checkUser);
            DataTable dtUser = new DataTable();
            sdUser.Fill(dtUser);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("ID already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else if(dtFname.Rows.Count > 0)
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
            else if (dtUser.Rows.Count > 0)
            {
                MessageBox.Show("User Name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                long contact = long.Parse(txtContact.Text);
                string Password = Cryptography.Encrypt(txtPassword.Text.ToString());
                SqlCommand cmd = new SqlCommand("Insert into users values ('" + txtID.Text + "', '" + txtFname.Text + "', '" + txtLname.Text + "', '" + txtAddress.Text + "', '" + contact + "', '" + txtUserName.Text + "', '" + Password + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Saved!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                con.Close();
                txtFname.Clear();
                txtLname.Clear();
                txtAddress.Clear();
                txtContact.Clear();
                txtID.Clear();

                loadDataGrid();

            }         
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            string name = txtFname.Text;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("Delete from users where fname= '" + name + "'", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cancelled!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            txtFname.Clear();
            txtLname.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtID.Clear();
   
       
            loadDataGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            txtFname.Text = dataGridView1.Rows[e.RowIndex].Cells["First Name"].Value.ToString();
            txtLname.Text = dataGridView1.Rows[e.RowIndex].Cells["Last Name"].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value.ToString();
            txtContact.Text = dataGridView1.Rows[e.RowIndex].Cells["Contact Number"].Value.ToString();
            txtUserName.Text = dataGridView1.Rows[e.RowIndex].Cells["Username"].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells["Password"].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from users where (fname like '%" + txtSearch.Text + "%') OR (lname like '%" + txtSearch.Text + "%') OR (address like '%" + txtSearch.Text + "%') OR (contact like '%" + txtSearch.Text + "%')", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }

        private void BorrowerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Brown;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
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
            SqlCommand check1 = new SqlCommand("select Idnum from users where UserName='" + txtUserName.Text + "'", con);
            string checkLastname = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkLastname == txtID.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataGrid();

            }
            else if (dtID.Rows.Count > 0)
            {
                MessageBox.Show("ID number already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET Idnum='" + int.Parse(txtID.Text) + "' where UserName = '" + txtUserName.Text + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("ID number Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

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
            SqlCommand check1 = new SqlCommand("select fname from users where UserName='" + txtUserName.Text + "'", con);
            string checkFirstname = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkFirstname == txtFname.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataGrid();


            }
            else if (dtFname.Rows.Count > 0)
            {
                MessageBox.Show("First name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET fname='" + txtFname.Text + "' where UserName = '" + txtUserName.Text + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("First Name Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

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
            SqlCommand check1 = new SqlCommand("select lname from users where UserName='" + txtUserName.Text + "'", con);
            string checkLastname = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkLastname == txtLname.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataGrid();


            }
            else if (dtLname.Rows.Count > 0)
            {
                MessageBox.Show("Last name already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET lname='" + txtLname.Text + "' where UserName = '" + txtUserName.Text + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Last Name Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
        }

        private void btnAddress_Click(object sender, EventArgs e)
        {
            con.Open();
            // Checking same value
            SqlCommand check1 = new SqlCommand("select address from users where UserName='" + txtUserName.Text + "'", con);
            string checkAddresss = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkAddresss == txtAddress.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataGrid();


            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET address='" + txtAddress.Text + "' where UserName = '" + txtUserName.Text + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Address Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            con.Open();
            // Checking same value
            SqlCommand checkContact = new SqlCommand("select contact from users where UserName='" + txtUserName.Text + "'", con);
            string check = Convert.ToString(checkContact.ExecuteScalar());
            con.Close();
            if (check == txtAddress.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataGrid();


            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET contact='" + long.Parse(txtContact.Text) + "' where UserName = '" + txtUserName.Text + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Contact Number Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
        }

        private void btnUsername_Click(object sender, EventArgs e)
        {
            con.Open();
            //Checking Username 
            SqlCommand checkUser = new SqlCommand("select * from users where UserName='" + txtUserName.Text + "'", con);
            SqlDataAdapter sdUser = new SqlDataAdapter(checkUser);
            DataTable dtUser = new DataTable();
            sdUser.Fill(dtUser);
            con.Close();
            // Checking same value
            con.Open();
            SqlCommand check1 = new SqlCommand("select lname from users where UserName='" + txtUserName.Text + "'", con);
            string checkLastname = Convert.ToString(check1.ExecuteScalar());
            con.Close();
            if (checkLastname == txtLname.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataGrid();


            }
            else if (dtUser.Rows.Count > 0)
            {
                MessageBox.Show("Username already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
            else
            {
                con.Open();
                SqlCommand cmdID = new SqlCommand("Update users SET UserName='" + txtUserName.Text + "' where Idnum = '" + int.Parse(txtID.Text) + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Username Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            con.Open();
            // Checking same value
            SqlCommand checkContact = new SqlCommand("select password from users where UserName='" + txtUserName.Text + "'", con);
            string check = Convert.ToString(checkContact.ExecuteScalar());
            con.Close();
            if (check == txtPassword.Text)
            {
                MessageBox.Show("You update the same value!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataGrid();


            }
            else
            {
                con.Open();
                string Password = Cryptography.Encrypt(txtPassword.Text.ToString());
                SqlCommand cmdID = new SqlCommand("Update users SET password='" + Password + "' where UserName = '" + txtUserName.Text + "'", con);
                cmdID.ExecuteNonQuery();
                MessageBox.Show("Password Updated Successfully!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                loadDataGrid();

            }
        }
    }
}
