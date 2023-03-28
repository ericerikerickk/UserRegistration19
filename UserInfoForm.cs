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
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            int id = int.Parse(txtID.Text);
            SqlCommand cmd = new SqlCommand("UPDATE users SET Idnum= '" + id + "', fname = '" + txtFname.Text + "', lname = '" + txtLname.Text + "', address= '" + txtAddress.Text + "', contact= '" + long.Parse(txtContact.Text) + "' where UserName= '" + username + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfuly updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
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
    }
}
    
  

