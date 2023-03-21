﻿using System;
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
    public partial class Form1 : Form
    {
        private string username;
        public Form1(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string usernameFormatted = username.Substring(0, 1).ToUpper() + username.Substring(1);
            label1.Text = $"Welcome, {usernameFormatted}!";
        }

        private void borrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserBorrow userBorrow = new UserBorrow(username);
            userBorrow.Show();
        }

        private void borrowerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }
    }
}