using Microsoft.EntityFrameworkCore.Migrations;

namespace Hypotec.Data.Migrations
{
    public partial class ColumnHowDidYouAboutUsInApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HowDidYouAboutUs",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HowDidYouAboutUs",
                table: "AspNetUsers");
        }
    }
}
