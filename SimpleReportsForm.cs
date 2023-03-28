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
    public partial class SimpleReportsForm : Form
    {
        public SimpleReportsForm()
        {
            InitializeComponent();
            loadDataGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select COUNT(Id) from users where UserName != 'admin'", con);
            Int32 result1 = Convert.ToInt32(cmd1.ExecuteScalar());
            con.Close();
            label6.Text = result1.ToString();
            con.Open();
            SqlCommand cmd2 = new SqlCommand("Select COUNT(available) from book WHERE available=0", con);
            Int32 result2 = Convert.ToInt32(cmd2.ExecuteScalar());
            con.Close();
            label7.Text = result2.ToString();
            con.Open();
            SqlCommand cmd3 = new SqlCommand("Select COUNT(available) from book WHERE available=1", con);
            Int32 result3 = Convert.ToInt32(cmd3.ExecuteScalar());
            con.Close();
            label8.Text = result3.ToString();
        }
    }
}
