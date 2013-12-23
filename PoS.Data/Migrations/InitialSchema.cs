using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Data.Migrations
{
    [Migration(0001)]
    public class InitialSchema : Migration
    {
        public override void Up()
        {
            #region Settings
            Create.Table("Settings")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ShopName").AsString().NotNullable()
                .WithColumn("Addess").AsString().NotNullable()
                .WithColumn("Phone").AsString().NotNullable()
                .WithColumn("TicketsPrinter").AsString().NotNullable()
                .WithColumn("ReportsPrinter").AsString().NotNullable();
            #endregion

            #region Customers
            Create.Table("Customers")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FullName").AsString().NotNullable()
                .WithColumn("TaxId").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("State").AsString().NotNullable()
                .WithColumn("City").AsString().NotNullable()
                .WithColumn("ZipCode").AsString().NotNullable();
            #endregion

            #region Categories
            Create.Table("Categories")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable();
            #endregion

            #region Products
            Create.Table("Products")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Barcode").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Price").AsDecimal(10, 2).NotNullable()
                .WithColumn("VAT").AsDecimal(10, 2).NotNullable()
                .WithColumn("CategoryId").AsInt32().NotNullable(); //If provider is SQLite
                
            Create.ForeignKey("fk_Products_CategoryId_Categories_Id")
                .FromTable("Products").ForeignColumn("CategoryId")
                .ToTable("Categories").PrimaryColumn("Id");
            #endregion

            #region Users
            Create.Table("Users")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("UserType").AsInt32().NotNullable();
            #endregion

            #region WorkShifts
            Create.Table("Workshifts")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Opening").AsDateTime().NotNullable()
                .WithColumn("Closing").AsDateTime().NotNullable()
                .WithColumn("CashAmount").AsDecimal(10, 2)
                .WithColumn("UserId").AsInt32().NotNullable(); //If provider is SQLite

            Create.ForeignKey("fk_Workshifts_UserId_Users_Id")
                .FromTable("Workshifts").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
            #endregion

            #region Sales
            Create.Table("Sales")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("TransactionNumber").AsString().NotNullable()
                .WithColumn("Total").AsDecimal(10, 2).NotNullable()
                .WithColumn("Amount").AsDecimal(10, 2).NotNullable()
                .WithColumn("Discount").AsDecimal(10, 2).NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable() //If provider is SQLite
                .WithColumn("CustomerId").AsInt32().NotNullable(); //If provider is SQLite

            Create.ForeignKey("fk_Sales_UserId_Users_Id")
                .FromTable("Sales").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");

            Create.ForeignKey("fk_Sales_CustomerId_Customers_Id")
                .FromTable("Sales").ForeignColumn("CustomerId")
                .ToTable("Customers").PrimaryColumn("Id");

            #region Details
            Create.Table("SalesDetails")
                .WithColumn("Id").AsInt32().NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable() //If provider is SQLite
                .WithColumn("SaleId").AsInt32().NotNullable() //If provider is SQLite
                .WithColumn("Qty").AsInt32().NotNullable();

            Create.ForeignKey("fk_SalesDetails_ProductId_Products_Id")
                .FromTable("SalesDetails").ForeignColumn("ProductId")
                .ToTable("Products").PrimaryColumn("Id");

            Create.ForeignKey("fk_SalesDetails_SaleId_Sales_Id")
                .FromTable("SalesDetails").ForeignColumn("SaleId")
                .ToTable("Sales").PrimaryColumn("Id");
            #endregion
            #endregion

            SeedData();
        }

        private void SeedData()
        {
            #region Customers
            Insert.IntoTable("Customers").Row(new { FullName = "General Public Sale", TaxId = " ", Address = " ", State = " ", City = " ", ZipCode = " " });
            #endregion

            #region Users
            Insert.IntoTable("Users").Row(new { Username = "Admin", Password = "admin", UserType = 1 });
            #endregion

            #region Categories
            Insert.IntoTable("Categories").Row(new { Name = "Candies" });
            #endregion

            #region Products
            Insert.IntoTable("Products").Row(new { Barcode = "074323077629", Name = "Kranky 15g", Description = "Hojuelas de maiz con cubierta sabor chocolate", Price = 10.00, VAT = 0.15, CategoryId = 1 });
            Insert.IntoTable("Products").Row(new { Barcode = "724869001748", Name = "Paleta MALVA BONY 12g", Description = "Malvavisco con covertura sabor chocolate", Price = 5.00, VAT = 0.15, CategoryId = 1 });
            Insert.IntoTable("Products").Row(new { Barcode = "02526719", Name = "Duvalin Tri-sabor 15g", Description = "Dulce cremoso sabor avellana, fresa y vainilla", Price = 7.00, VAT = 0.15, CategoryId = 1 });
            #endregion
        }

        public override void Down()
        {
            Delete.Table("Sales");

            Delete.Table("SalesDetails");

            Delete.Table("Workshifts");
            
            Delete.Table("Users");
            
            Delete.Table("Products");
            
            Delete.Table("Categories");
            
            Delete.Table("Customers");
            
            Delete.Table("Settings");
        }
    }
}
