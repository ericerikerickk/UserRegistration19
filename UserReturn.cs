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
    public partial class UserReturn : Form
    {
        DateTime myDateTime = DateTime.Now;
        private string username;
        public UserReturn(string username)
        {
            InitializeComponent();
            this.username = username;
            loadDataGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select book.accession_number, book.title, book.author, borrowTable.borrowDate from book INNER JOIN borrowTable ON book.accession_number = borrowTable.bookID where borrowTable.username = '" + username + "' AND book.available = 0", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
    }
}
