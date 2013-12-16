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
            Create.Table("Configurations")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ShopName").AsString(255).NotNullable()
                .WithColumn("Addess").AsString(255).NotNullable()
                .WithColumn("Phone").AsString(255).NotNullable()
                .WithColumn("TicketsPrinter").AsString(255).NotNullable()
                .WithColumn("ReportsPrinter").AsString(255).NotNullable();

            Create.Table("Categories")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).NotNullable();

            Create.Table("Customers")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FullName").AsString(255).NotNullable()
                .WithColumn("Address").AsString(255).NotNullable()
                .WithColumn("State").AsString(255).NotNullable()
                .WithColumn("City").AsString(255).NotNullable()
                .WithColumn("ZipCode").AsString(255).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Customers");
            Delete.Table("Categories");
            Delete.Table("Configurations");
        }
    }
}
