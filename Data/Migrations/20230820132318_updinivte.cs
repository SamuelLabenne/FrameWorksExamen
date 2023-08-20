using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrameWorksExamen.Data.Migrations
{
    /// <inheritdoc />
    public partial class updinivte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Invite",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_SenderId",
                table: "Invite",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_AspNetUsers_SenderId",
                table: "Invite",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invite_AspNetUsers_SenderId",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_SenderId",
                table: "Invite");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Invite");
        }
    }
}
