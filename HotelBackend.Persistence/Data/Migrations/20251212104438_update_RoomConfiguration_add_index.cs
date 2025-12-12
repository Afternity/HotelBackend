using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBackend.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_RoomConfiguration_add_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Number",
                table: "Rooms");
        }
    }
}
