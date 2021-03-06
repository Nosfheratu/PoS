﻿using System;
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

        CustomersService customersService;
        ProductsService productsService;
        SalesService salesService;

        public frmSales()
        {
            InitializeComponent();
        }

        public frmSales(User user)
        {
            InitializeComponent();
            
            this.User = user;

            customersService = new CustomersService();
            productsService = new ProductsService();
            salesService = new SalesService();

            this.Load += (s, e) =>
            {
                this.WindowState = FormWindowState.Maximized;
                StartNewSale();
            };

            this.KeyDown += (s, e) =>
            {

            };
            
            txtBarcode.KeyDown += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        AddItemToList();
                        break;
                    case Keys.F4:
                        listView1.Focus();
                        break;
                    case Keys.F6:
                        Charge();
                        break;
                }
            };

            listView1.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                    RemoveItemFromList();

                if (e.KeyCode == Keys.F4)
                {
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                }

                if (e.KeyCode == Keys.F6)
                    Charge();
            };

            listView1.SelectedIndexChanged += (s, e) =>
            {
                btnRemoveItem.Enabled = true;
            };

            btnAddItem.Click += (s, e) =>
            {
                AddItemToList();
            };

            btnRemoveItem.Click += (s, e) =>
            {
                RemoveItemFromList();
            };

            btnResetSale.Click += (s, e) =>
            {
                ResetSale();
            };

            btnCharge.Click += (s, e) =>
            {
                Charge();
            };
        }
        
        private void StartNewSale()
        {
            var customer = customersService.GetDefaultCustomer();

            txtTaxID.Text = customer.TaxpayerID;
            txtName.Text = customer.FullName;
            txtAddress.Text = customer.Address;

            listView1.Items.Clear();

            txtQty.Text = "1";
            txtDate.Text = DateTime.Now.ToShortDateString();
            txtDiscount.Text = txtSubtotal.Text = txtTotal.Text = "0";
            txtTax.Text = "0.15";

            txtBarcode.Text = "";
            txtBarcode.Focus();
        }

        private void AddItemToList()
        {
            var product = productsService.Find(txtBarcode.Text);

            if (product != null && IsInteger(txtQty.Text))
            {
                ListViewItem item = new ListViewItem(product.Barcode);
                item.SubItems.Add(txtQty.Text);
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.VAT.ToString());
                item.SubItems.Add(product.Price.ToString());
                decimal subtotal = product.Price * Convert.ToInt32(txtQty.Text);
                item.SubItems.Add(subtotal.ToString());

                listView1.Items.Add(item);

                CalculateSubtotal();
                CalculateTotal();

                txtBarcode.Text = "";
                txtBarcode.Focus();
            }
        }

        private void RemoveItemFromList()
        {
            if (listView1.SelectedItems.Count > 0)
                foreach (ListViewItem item in listView1.SelectedItems)
                    listView1.Items.Remove(item);

            CalculateSubtotal();
            CalculateTotal();

            btnRemoveItem.Enabled = false;
            txtBarcode.Focus();
        }

        private void ResetSale()
        {
            listView1.Items.Clear();

            CalculateSubtotal();
            CalculateTotal();
        }

        private void Charge()
        {
            if (listView1.Items.Count == 0)
                return;

            var sale = new Sale();
            
            sale.SaleDetails = SaleDetails(listView1.Items);

            var customer = customersService.Find(txtTaxID.Text);

            var frmCharge = new frmCharge(this.User, customer, sale, Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtDiscount.Text), Convert.ToDecimal(txtTotal.Text));
            frmCharge.ShowDialog();

            if (frmCharge.DialogResult == System.Windows.Forms.DialogResult.OK)
                StartNewSale();
            else if (frmCharge.DialogResult == System.Windows.Forms.DialogResult.No)
                frmCharge.Dispose();
        }

        private List<SaleDetail> SaleDetails(ListView.ListViewItemCollection items)
        {
            var products = new List<SaleDetail>();

            foreach (ListViewItem item in items)
            {
                var product = productsService.Find(item.SubItems[0].Text);
                int qty = Convert.ToInt32(item.SubItems[1].Text);

                products.Add(new SaleDetail { ProductId = product.Id, Qty = qty });
            }

            return products;
        }

        private void CalculateSubtotal()
        {
            decimal subtotal = 0;

            foreach (ListViewItem item in listView1.Items)
                subtotal += Convert.ToDecimal(item.SubItems[5].Text);

            txtSubtotal.Text = subtotal.ToString();
        }

        private void CalculateTotal()
        {
            decimal total = 0;

            total = Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDiscount.Text);

            txtTotal.Text = total.ToString();
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
