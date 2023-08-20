using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrameWorksExamen.Data.Migrations
{
    /// <inheritdoc />
    public partial class deletepeeps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "people",
                table: "Event");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "people",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
