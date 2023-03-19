using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserRegistration19
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BookForm frm3 = new BookForm();
            frm3.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void borrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorrowerForm borrow = new BorrowerForm();
            borrow.Show();
        }

        private void borrowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BorrowForm decrease = new BorrowForm();
            decrease.Show();
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnForm returnForm = new ReturnForm();
            returnForm.Show();
        }

        private void dateFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateFilterForm dateForm = new DateFilterForm();
            dateForm.Show();
        }

        private void simpleReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimpleReportsForm simpleReportForm = new SimpleReportsForm();
            simpleReportForm.Show();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }

        private void booksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintForm printbooks = new PrintForm();
            printbooks.Show();
        }

        private void borrowersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintBorrowers printBorrow = new PrintBorrowers();
            printBorrow.Show();
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }
    }
}
