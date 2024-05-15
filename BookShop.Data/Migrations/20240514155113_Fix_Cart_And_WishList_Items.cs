using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Cart_And_WishList_Items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId",
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
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId",
                unique: true);
        }
    }
}
