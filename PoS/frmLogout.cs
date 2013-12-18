using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PoS.Entities;

namespace PoS
{
    public partial class frmLogout : Form
    {
        public frmMain frmMain;
        User User;
        bool Logged { get; set; }

        public frmLogout()
        {
            InitializeComponent();
        }

        public frmLogout(User user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void frmLogout_Load(object sender, EventArgs e)
        {
            var users = new List<User>(1);
            users.Add(this.User);

            cmbUsers.DataSource = users;
            cmbUsers.DisplayMember = "Username";
            cmbUsers.ValueMember = "Id";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.User.ValidatePassword(txtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                this.DialogResult = DialogResult.None;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void frmLogout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.User.ValidatePassword(txtPassword.Text))
                this.DialogResult = DialogResult.No;
        }
    }
}
