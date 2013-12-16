using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Data.Migrations
{
    [Migration(0001)]
    public class CreateConfigurationsTable : Migration
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
        }

        public override void Down()
        {
            Delete.Table("Configurations");
        }
    }
}
