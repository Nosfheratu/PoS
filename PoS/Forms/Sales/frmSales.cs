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

namespace PoS.Forms.Sales
{
    public partial class frmSales : Form
    {
        User User;

        UsersService usersService;
        CustomersService customersService;

        public frmSales()
        {
            InitializeComponent();
        }

        public frmSales(User user)
        {
            InitializeComponent();
            
            this.User = user;

            usersService = new UsersService();
            customersService = new CustomersService();
        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            StarNewSale();
        }

        private void StarNewSale()
        {
            var customer = customersService.GetDefaultCustomer();

            txtTaxId.Text = customer.TaxId;
            txtName.Text = customer.FullName;
            txtAddress.Text = customer.Address;

            listView1.Items.Clear();

            txtQty.Text = "1";
            txtDate.Text = DateTime.Now.ToShortDateString();
            txtDiscount.Text = "0";

            txtBarcode.Focus();
        }

        #region Helpers
        static bool IsInteger(string number)
        {
            try
            {
                Convert.ToInt32(number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool IsDecimal(string number)
        {
            try
            {
                Convert.ToDecimal(number);
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
