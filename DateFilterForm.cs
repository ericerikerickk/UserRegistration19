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
    public partial class DateFilterForm : Form
    {
        public DateFilterForm()
        {
            InitializeComponent();           
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            con.Open();

            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author], returnDate AS [Returned Date] from book where returnDate between '" + date1 + "' AND '" + date2 + "'order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
        private void loadDataGrid2()
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            con.Open();

            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author], borrowedDate AS [Borrowed Date] from book where borrowedDate between '" + date1 + "' AND '" + date2 + "'order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
        private void loadDataGrid3()
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            con.Open();

            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author], addedDate AS [Added Date] from book where addedDate between '" + date1 + "' AND '" + date2 + "'order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadDataGrid();
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            loadDataGrid2();
        }

        private void btnSearch3_Click(object sender, EventArgs e)
        {
            loadDataGrid3();
        }
    }
}
