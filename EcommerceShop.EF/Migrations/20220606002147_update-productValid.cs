using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShop.EF.Migrations
{
    public partial class updateproductValid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureURL",
                table: "products",
                newName: "PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "products",
                newName: "PictureURL");
        }
    }
}
