using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPropertyCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedCode",
                table: "Customer",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedCodeExpiredTime",
                table: "Customer",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedCode",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ApprovedCodeExpiredTime",
                table: "Customer");
        }
    }
}
