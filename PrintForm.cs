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
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
            loadDataGrid();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=LibSysDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void loadDataGrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select accession_number AS [Accession Number], title AS [Title], author AS [Author], convert(date,addedDate,105) as [Added Date], returnDate as [Return Date], borrowedDate as [Borrowed Date] from book order by accession_number asc", con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            dataGridView1.DataSource = tab;

            con.Close();
        }

        Bitmap bitmap;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            this.Controls.Add(panel);

            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, size.Height, graphics);
            graphics = Graphics.FromImage(bitmap);

            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.Columns["Return Date"].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
            txtAcc.Text = dataGridView1.Rows[e.RowIndex].Cells["Accession Number"].Value.ToString();
            txtTitle.Text = dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString();
            txtAuthor.Text = dataGridView1.Rows[e.RowIndex].Cells["Author"].Value.ToString();
            txtReturnDate.Text = dataGridView1.Rows[e.RowIndex].Cells["Return Date"].Value.ToString();
            txtAddedDate.Text = dataGridView1.Rows[e.RowIndex].Cells["Added Date"].Value.ToString();
            txtBorrowedDate.Text = dataGridView1.Rows[e.RowIndex].Cells["Borrowed Date"].Value.ToString();
        }
    }
}
