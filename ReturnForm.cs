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
    public partial class ReturnForm : Form
    {
        DateTime myDateTime = DateTime.Now;

        public ReturnForm()
        {
            InitializeComponent();
            loadDataGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void loadDataGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author] from book where available=0 order by accession_number asc", con);
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            int number = int.Parse(lblAccession.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update book SET available=1, returnDate= '" + myDateTime + "' where accession_number= '" + number + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Returned!, You returned a book on " + myDateTime + ".", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            loadDataGrid();
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Brown;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
    }
}
