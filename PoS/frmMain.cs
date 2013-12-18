using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            OpenTill();
        }
        
        private void btnOpenTill_Click(object sender, EventArgs e)
        {
            OpenTill();
        }

        private void btnCloseTill_Click(object sender, EventArgs e)
        {
            CloseTill();
        }

        private void OpenTill()
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.frmMain = this;
            frmLogin.ShowDialog();
        }

        private void CloseTill()
        {
        }
    }
}
