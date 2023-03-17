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
    public partial class BorrowForm : Form
    {
        DateTime myDateTime = DateTime.Now;

        public BorrowForm()
        {
            InitializeComponent();
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblAccession.Text = dataGridView1.Rows[e.RowIndex].Cells["Accession Number"].Value.ToString();
            lblTitle.Text = dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            lblAuthor.Text = dataGridView1.Rows[e.RowIndex].Cells["Author"].Value.ToString();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            int number = int.Parse(lblAccession.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update book SET available=0, date= '" + myDateTime + "' where accession_number= '" + number + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Borrowed!, You borrowed a book on " + myDateTime +".", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            loadDataGrid();
            lblAccession.Text = "";
            lblTitle.Text = "";
            lblAuthor.Text = "";
        }
    }
}
