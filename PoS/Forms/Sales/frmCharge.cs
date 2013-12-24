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
    public partial class frmCharge : Form
    {
        User User;
        Customer Customer;

        Sale Sale;

        SalesService salesService;

        public frmCharge()
        {
            InitializeComponent();
        }

        public frmCharge(User user, Customer customer, Sale sale, decimal amount, decimal discount, decimal total)
        {
            InitializeComponent();

            this.User = user;
            this.Customer = customer;
            sale.Amount = amount;
            sale.Discount = discount;
            sale.Total = total;
            this.Sale = sale;

            this.salesService = new SalesService();

            this.Load += (s, e) =>
            {
                txtTicket.Text = salesService.GetTicketNumber().ToString();
                txtDiscout.Text = this.Sale.Discount.ToString();
                txtTotal.Text = this.Sale.Total.ToString();

                cmbUser.DataSource = new List<User> { this.User };
                cmbUser.DisplayMember = "Username";
                cmbUser.ValueMember = "Id";

                cmbCustomer.DataSource = new List<Customer> { this.Customer };
                cmbCustomer.DisplayMember = "FullName";
                cmbCustomer.ValueMember = "Id";

                txtCash.Focus();
            };

            btnCharge.Click += (s, e) =>
            {
                var currentSale = this.Sale;

                salesService.Create(currentSale, this.User, this.Customer);

                //
                //PrintTicket
                //
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            };

            txtCash.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var currentSale = this.Sale;

                    salesService.Create(currentSale, this.User, this.Customer);

                    //
                    //PrintTicket
                    //
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            };

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            };
        }
    }
}
