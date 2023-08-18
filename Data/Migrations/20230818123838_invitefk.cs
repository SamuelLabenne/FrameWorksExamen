using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrameWorksExamen.Data.Migrations
{
    /// <inheritdoc />
    public partial class invitefk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateIndex(
                name: "IX_Invite_EventId",
                table: "Invite",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_PersonId",
                table: "Invite",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Event_EventId",
                table: "Invite",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Person_PersonId",
                table: "Invite",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Event_EventId",
                table: "Invite");

            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Person_PersonId",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_EventId",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_PersonId",
                table: "Invite");


        }
    }
}
