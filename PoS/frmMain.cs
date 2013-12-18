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
    public partial class frmMain : Form
    {
        UsersService usersService;
        WorkshiftsService workshisftService;

        public frmMain()
        {
            InitializeComponent();

            usersService = new UsersService();
            workshisftService = new WorkshiftsService();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToShortDateString();
            
            ValidateSettings();
            
            ValidateTills();
            
            BeginSales();
        }
        
        private void btnOpenTill_Click(object sender, EventArgs e)
        {
            OpenTill();
        }

        private void btnCloseTill_Click(object sender, EventArgs e)
        {
            CloseTill();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool openedTill = workshisftService.OpenedTill();

            if (openedTill)
                MessageBox.Show("Opened Till. Next time you run the system you will continue with last sesion.");

            this.Dispose();
        }

        private void ValidateSettings()
        {

        }

        private void ValidateTills()
        {
            var user = workshisftService.GetUserByOpenedTill();
            var workshift = user.Workshifts.Last();
            
            if (workshift.Opened)
            {
                btnCloseTill.Enabled = true;
                btnOpenTill.Enabled = false;
                
                lblNombreUsuario.Text = user.Username;
                lblUserId.Text = user.Id.ToString();
            }
            else
                OpenTill();
        }

        private void BeginSales()
        {

        }

        private void OpenTill()
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.frmMain = this;
            frmLogin.ShowDialog();

            if (frmLogin.DialogResult == DialogResult.OK)
            {
                var user = frmLogin.User;

                btnOpenTill.Enabled = false;
                
                lblUserId.Text = user.Id.ToString();
                lblNombreUsuario.Text = user.Username;
                
                OpenTillForUser(user);
                
                btnCloseTill.Enabled = true;
            }
            else if (frmLogin.DialogResult == DialogResult.No)
            {
                btnOpenTill.Enabled = true;
                btnCloseTill.Enabled = false;
            }
        }

        private void CloseTill()
        {
            frmLogout frmLogout = new frmLogout(usersService.Find(Convert.ToInt32(lblUserId.Text)));
            frmLogout.frmMain = this;
            frmLogout.ShowDialog();

            if (frmLogout.DialogResult == DialogResult.OK)
            {
                btnOpenTill.Enabled = true;
                
                lblNombreUsuario.Text = "-";
                lblUserId.Text = "-";

                workshisftService.CloseLastWorkshift();
                
                btnCloseTill.Enabled = false;
            }
            else if (frmLogout.DialogResult == DialogResult.No)
            {
                btnOpenTill.Enabled = false;
                btnCloseTill.Enabled = true;
            }
        }

        private void OpenTillForUser(User user)
        {
            var tillAmount = new frmTillAmount();
            tillAmount.frmMain = this;
            tillAmount.ShowDialog();

            workshisftService.CreateWorkshift(user, tillAmount.Amount);

            MessageBox.Show("Till Opened. Now your can star sales process.");
        }
    }
}
