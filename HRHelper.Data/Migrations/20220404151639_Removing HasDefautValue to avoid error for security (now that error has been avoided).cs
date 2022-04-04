using Microsoft.EntityFrameworkCore.Migrations;

namespace HRHelper.Data.Migrations
{
    public partial class RemovingHasDefautValuetoavoiderrorforsecuritynowthaterrorhasbeenavoided : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "password",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
