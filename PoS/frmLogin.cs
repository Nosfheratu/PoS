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
using PoS.Services;

namespace PoS
{
    public partial class frmLogin : Form
    {
        public frmMain frmMain;

        public int UserId { get { return Convert.ToInt32(cmbUsers.SelectedValue); } }

        bool Logged { get; set; }

        UsersService _usersService;

        public frmLogin()
        {
            InitializeComponent();

            _usersService = new UsersService();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Logged = Login();

            if (Logged)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                this.DialogResult = DialogResult.No;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Logged)
                this.DialogResult = DialogResult.No;
        }

        private void LoadUsers()
        {
            cmbUsers.DataSource = _usersService.GetAll();
            cmbUsers.DisplayMember = "Username";
            cmbUsers.ValueMember = "Id";
        }

        private bool Login()
        {
            return _usersService.Login(Convert.ToInt32(cmbUsers.SelectedValue), txtPassword.Text);
        }
    }
}
