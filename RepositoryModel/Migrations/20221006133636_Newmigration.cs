using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryModel.Migrations
{
    public partial class Newmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTables",
                table: "UserTables");

            migrationBuilder.RenameTable(
                name: "UserTables",
                newName: "UserTableDB");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "UserTableDB",
                newName: "EmailID");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserTableDB",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTableDB",
                table: "UserTableDB",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTableDB",
                table: "UserTableDB");

            migrationBuilder.RenameTable(
                name: "UserTableDB",
                newName: "UserTables");

            migrationBuilder.RenameColumn(
                name: "EmailID",
                table: "UserTables",
                newName: "EmailId");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserTables",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTables",
                table: "UserTables",
                column: "UserId");
        }
    }
}
