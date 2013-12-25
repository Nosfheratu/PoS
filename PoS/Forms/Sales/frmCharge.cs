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
using PoS.Tickets;

namespace PoS.Forms.Sales
{
    public partial class frmCharge : Form
    {
        User User;
        Customer Customer;

        Sale Sale;

        SalesService salesService;
        ProductsService productsService;

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
            this.productsService = new ProductsService();

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
                PrintTicket(this.Sale);

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
                    PrintTicket(this.Sale);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            };

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            };
        }

        private void PrintTicket(Sale sale)
        {
            var ticket = new Ticket();
            ticket.MaxChar = 30;
            ticket.MaxCharDescription = 15;

            ticket.AddHeaderLine("[Shop Name Here]");
            ticket.AddHeaderLine("[Taxpayer ID Here]");
            ticket.AddHeaderLine("[Shop Address Here]");
            ticket.AddHeaderLine("[Shop Phone Here]");
            ticket.AddHeaderLine("");
            ticket.AddHeaderLine("[Customer Name Here]");

            ticket.AddSubHeaderLine("[Ticket Number Here*");
            ticket.AddSubHeaderLine("[Sales User Name Here]");
            ticket.AddSubHeaderLine("[Current Date and Time Here]");

            decimal subtotal = sale.Amount;
            int productNumer = 0;

            sale.SaleDetails.ToList().ForEach(sd =>
            {
                productNumer += sd.Qty;
                var product = productsService.Find(sd.ProductId);

                ticket.AddItem(sd.Qty.ToString(), product.Name, product.Price.ToString("C"));
            });

            ticket.AddTotal("Amount : ", sale.Amount.ToString("C"));
            ticket.AddTotal("", "");
            ticket.AddTotal("Tax : ", "[Taxs Here]");
            ticket.AddTotal("", "");
            ticket.AddTotal("Dicount : ", sale.Discount.ToString("C"));
            ticket.AddTotal("", "----------");
            ticket.AddTotal("TOTAL : ", sale.Total.ToString("C"));
            ticket.AddTotal("Cash : ", string.Format(txtCash.Text, "C"));
            ticket.AddTotal("Change : ", string.Format(txtChange.Text, "C"));

            ticket.AddFooterLine("");
            ticket.AddFooterLine(string.Format("Products number: {0}", productNumer.ToString()));

            if (ticket.PrinterExists("doPDF v7"))
                ticket.PrintTicket("doPDF v7");
        }
    }
}
