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
    public partial class frmTillAmount : Form
    {
        public frmMain frmMain;

        public decimal Amount { get { return Convert.ToDecimal(txtAmount.Text); } }

        public frmTillAmount()
        {
            InitializeComponent();
        }

        private void frmWorkshift_Load(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWorkshift_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsDecimal(txtAmount.Text))
                e.Cancel = true;
        }

        #region Helpers
        static bool IsDecimal(string value)
        {
            try
            {
                Convert.ToDecimal(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool IsInteger(string value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
