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
    }
}
