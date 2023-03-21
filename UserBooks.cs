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
    public partial class UserBooks : Form
    {
        private string username;

        public UserBooks(string username)
        {
            InitializeComponent();
            this.username = username;
            loadDataGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void loadDataGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DISTINCT book.accession_number AS [Accession Number], book.title AS [Title], book.author AS [Author] from book INNER JOIN borrowTable ON book.accession_number = borrowTable.bookID where borrowTable.username = '" + username + "' AND book.available = 0", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAccession.Text = dataGridView1.Rows[e.RowIndex].Cells["Accession Number"].Value.ToString();
            lblTitle.Text = dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            lblAuthor.Text = dataGridView1.Rows[e.RowIndex].Cells["Author"].Value.ToString();
        }

        private void UserBooks_Load(object sender, EventArgs e)
        {
            string usernameFormatted = username.Substring(0, 1).ToUpper() + username.Substring(1);
            label2.Text = $"{usernameFormatted}'s Books";
        }
    }
}
