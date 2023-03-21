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
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE users SET fname = '" + txtFname.Text + "', lname = '" + txtLname.Text + "', address= '" + txtAddress.Text + "', contact= '" + long.Parse(txtContact.Text) + "' where UserName= '" + username + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfuly updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
