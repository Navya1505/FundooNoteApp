using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryModel.Migrations
{
    public partial class Thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaborateTableDB",
                columns: table => new
                {
                    CollabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sender_UserId = table.Column<long>(nullable: false),
                    Receiver_UserId = table.Column<long>(nullable: false),
                    Sender_Email = table.Column<string>(nullable: true),
                    Receiver_Email = table.Column<string>(nullable: true),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborateTableDB", x => x.CollabId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaborateTableDB");
        }
    }
}
