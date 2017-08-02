using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ModelDefinition.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Company",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Company", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Phones",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            ManufacturerId = table.Column<int>(type: "int", nullable: true),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            Price = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Phones", x => x.Id);
            table.ForeignKey(
                      name: "FK_Phones_Company_ManufacturerId",
                      column: x => x.ManufacturerId,
                      principalTable: "Company",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Phones_ManufacturerId",
          table: "Phones",
          column: "ManufacturerId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Phones");

      migrationBuilder.DropTable(
          name: "Company");
    }
  }
}
