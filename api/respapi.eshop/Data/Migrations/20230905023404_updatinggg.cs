using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace respapi.eshop.data.migrations
{
    /// <inheritdoc />
    public partial class updatinggg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAdress_AspNetUsers_AppUserId",
                table: "UserAdress");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAdress",
                table: "UserAdress");

            migrationBuilder.RenameTable(
                name: "UserAdress",
                newName: "UserAdresses");

            migrationBuilder.RenameIndex(
                name: "IX_UserAdress_AppUserId",
                table: "UserAdresses",
                newName: "IX_UserAdresses_AppUserId");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAdresses",
                table: "UserAdresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdresses_AspNetUsers_AppUserId",
                table: "UserAdresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAdresses_AspNetUsers_AppUserId",
                table: "UserAdresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAdresses",
                table: "UserAdresses");

            migrationBuilder.RenameTable(
                name: "UserAdresses",
                newName: "UserAdress");

            migrationBuilder.RenameIndex(
                name: "IX_UserAdresses_AppUserId",
                table: "UserAdress",
                newName: "IX_UserAdress_AppUserId");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAdress",
                table: "UserAdress",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.SubCategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdress_AspNetUsers_AppUserId",
                table: "UserAdress",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
