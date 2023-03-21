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
    public partial class UserBorrow : Form
    {
        private string username;
        DateTime myDateTime = DateTime.Now;

        public UserBorrow(string username)
        {
            InitializeComponent();
            this.username = username;
            loadDataGrid();

        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author] from book where available=1 order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
        private void UserBorrow_Load(object sender, EventArgs e)
        {
            string usernameFormatted = username.Substring(0, 1).ToUpper() + username.Substring(1);
            label1.Text = usernameFormatted;
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into borrowTable (username, bookID, borrowDate) values ('" + username + "', '" + lblAccession.Text +  "', '" + myDateTime + "')", con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("Update book SET available=0 where accession_number= '" + lblAccession.Text + "'", con);
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Successfully Borrowed", "Window", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            loadDataGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAccession.Text = dataGridView1.Rows[e.RowIndex].Cells["Accession Number"].Value.ToString();
            lblTitle.Text = dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            lblAuthor.Text = dataGridView1.Rows[e.RowIndex].Cells["Author"].Value.ToString();
        }
    }
}
