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
    public partial class BookForm : Form
    {
        DateTime myDateTime = DateTime.Now;

        public BookForm()
        {
            InitializeComponent();

            txtSearch.Text = "Search Here...";
            txtSearch2.Text = "Search Here...";
            loadDataGrid();
            loadDataGrid2();
        }
        
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author], addedDate as [Added Date], returnDate as [Return Date] from book where available=1 order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
        private void loadDataGrid2()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author], addedDate as [Added Date], borrowedDate as [Borrowed Date] from book where available=0 order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView2.DataSource = tab;

            con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand checkAcc = new SqlCommand("select accession_number from book where accession_number='" + int.Parse(txtno.Text) + "'", con);
            SqlDataAdapter sdAcc = new SqlDataAdapter(checkAcc);
            DataTable dtAcc = new DataTable();
            sdAcc.Fill(dtAcc);
            if(dtAcc.Rows.Count > 0)
            {
                MessageBox.Show("Accession Number already Exist!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Insert into book (accession_number, title, author, addedDate) values ('" + txtno.Text + "', '" + txttitle.Text + "', '" + txtauthor.Text + "', '" + myDateTime + "')", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Saved!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                con.Close();
                loadDataGrid();
                loadDataGrid2();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author] from book where title like '%" + txtSearch.Text + "%' AND available=1", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = dataGridView1.Rows[e.RowIndex].Cells["Accession Number"].Value.ToString();
            txttitle.Text = dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            txtauthor.Text = dataGridView1.Rows[e.RowIndex].Cells["Author"].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            con.Open();
            string no;
            no = txtno.Text;

            SqlCommand cmd = new SqlCommand("Update book SET title= '" + txttitle.Text + "', author='" + txtauthor.Text + "' where accession_number= '" + no + "'", con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully Updated!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();
            loadDataGrid();
            loadDataGrid2();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            int num = (int.Parse(txtno.Text));

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("Delete from book where accession_number= '" + num + "'", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cancelled!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            loadDataGrid();
            loadDataGrid2();
        }

        private void BookForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Brown;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;

            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Brown;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accesion Number], title AS [Title], author AS [Author] from book where title like '%" + txtSearch.Text + "%' AND available=0", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView2.DataSource = tab;

            con.Close();
            loadDataGrid();
            loadDataGrid2();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search Here...")
            {
                txtSearch.Text = "";
            }
            loadDataGrid();
            loadDataGrid2();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Search Here...";
            }
            loadDataGrid();
            loadDataGrid2();
        }

        private void txtSearch2_Enter(object sender, EventArgs e)
        {
            if (txtSearch2.Text == "Search Here...")
            {
                txtSearch2.Text = "";
            }
            loadDataGrid();
            loadDataGrid2();
        }

        private void txtSearch2_Leave(object sender, EventArgs e)
        {
            if (txtSearch2.Text == "")
            {
                txtSearch2.Text = "Search Here...";
            }
            loadDataGrid();
            loadDataGrid2();
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("You cant edit! ", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
