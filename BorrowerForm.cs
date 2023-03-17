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
    public partial class BorrowerForm : Form
    {
        public BorrowerForm()
        {
            InitializeComponent();
            loadDataGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("Select fname AS [First Name], lname AS [Last Name], address AS [Address], contact AS [Contact Number] from users order by Id asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            long contact = long.Parse(txtContact.Text);
            SqlCommand cmd = new SqlCommand("Insert into users values ('" + txtID.Text + "', '" + txtFname.Text + "', '" + txtLname.Text + "', '" + txtAddress.Text + "', '" + contact + "')", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Saved!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            loadDataGrid();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            con.Open();
            long contact = long.Parse(txtContact.Text);
            int id = int.Parse(txtID.Text);
            SqlCommand cmd = new SqlCommand("Update users SET fname= '" + txtFname.Text + "', lname= '" + txtLname.Text + "', address='" + txtAddress.Text + "', contact='" + contact + "' where Id= '" + id + "'", con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully Updated!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();
            loadDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            string name = txtFname.Text;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("Delete from users where fname= '" + name + "'", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cancelled!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            loadDataGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFname.Text = dataGridView1.Rows[e.RowIndex].Cells["First Name"].Value.ToString();
            txtLname.Text = dataGridView1.Rows[e.RowIndex].Cells["Last Name"].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value.ToString();
            txtContact.Text = dataGridView1.Rows[e.RowIndex].Cells["Contact Number"].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from users where (fname like '%" + txtSearch.Text + "%') OR (lname like '%" + txtSearch.Text + "%') OR (address like '%" + txtSearch.Text + "%') OR (contact like '%" + txtSearch.Text + "%')", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }
    }
}
