using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrameWorksExamen.Data.Migrations
{
    /// <inheritdoc />
    public partial class veelopveel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventPerson",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    InvitedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPerson", x => new { x.EventsId, x.InvitedId });
                    table.ForeignKey(
                        name: "FK_EventPerson_Event_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPerson_Person_InvitedId",
                        column: x => x.InvitedId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPerson_InvitedId",
                table: "EventPerson",
                column: "InvitedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventPerson");
        }
    }
}
