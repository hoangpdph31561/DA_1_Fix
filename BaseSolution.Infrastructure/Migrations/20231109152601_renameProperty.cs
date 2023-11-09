using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renameProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBookingNeed",
                table: "Service",
                newName: "IsRoomBookingNeed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRoomBookingNeed",
                table: "Service",
                newName: "IsBookingNeed");
        }
    }
}
