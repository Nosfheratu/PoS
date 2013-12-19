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
                .WithColumn("VAT").AsDecimal(10, 2).NotNullable();
                
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
                .WithColumn("UserId").AsInt32().NotNullable();

            Create.ForeignKey("fk_Workshifts_UserId_Users_Id")
                .FromTable("Workshifts").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
            #endregion

            #region Purchases
            Create.Table("Purchases")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("TransactionNumber").AsString().NotNullable()
                .WithColumn("Total").AsDecimal().NotNullable()
                .WithColumn("Discount").AsDecimal().NotNullable();

            Create.ForeignKey("fk_Purchases_ProductId_Products_Id")
                .FromTable("Purchases").ForeignColumn("ProductId")
                .ToTable("Products").PrimaryColumn("Id");

            Create.ForeignKey("fk_Purchases_UserId_Users_Id")
                .FromTable("Purchases").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");

            Create.ForeignKey("fk_Purchases_CustomerId_Customers_Id")
                .FromTable("Purchases").ForeignColumn("CustomerId")
                .ToTable("Customers").PrimaryColumn("Id");
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
        }

        public override void Down()
        {
            Delete.Table("Purchases");

            Delete.Table("Workshifts");
            
            Delete.Table("Users");
            
            Delete.Table("Products");
            
            Delete.Table("Categories");
            
            Delete.Table("Customers");
            
            Delete.Table("Settings");
        }
    }
}
