using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishListItems_ProductId_WishListId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId_CartId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId_WishListId",
                table: "WishListItems",
                columns: new[] { "ProductId", "WishListId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId_CartId",
                table: "CartItems",
                columns: new[] { "ProductId", "CartId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishListItems_ProductId_WishListId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId_CartId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId_WishListId",
                table: "WishListItems",
                columns: new[] { "ProductId", "WishListId" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId_CartId",
                table: "CartItems",
                columns: new[] { "ProductId", "CartId" });
        }
    }
}
