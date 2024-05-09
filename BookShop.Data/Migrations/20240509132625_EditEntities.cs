using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Clients_ClientEntityId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Clients_ClientId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ClientEntityId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ClientEntityId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "WishListItems",
                newName: "ClientEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_WishListItems_ClientId",
                table: "WishListItems",
                newName: "IX_WishListItems_ClientEntityId");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "PaymentMethods",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Clients_ClientEntityId",
                table: "WishListItems",
                column: "ClientEntityId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Clients_ClientEntityId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "ClientEntityId",
                table: "WishListItems",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_WishListItems_ClientEntityId",
                table: "WishListItems",
                newName: "IX_WishListItems_ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "PaymentMethods",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<long>(
                name: "ClientEntityId",
                table: "CartItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ClientEntityId",
                table: "CartItems",
                column: "ClientEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Clients_ClientEntityId",
                table: "CartItems",
                column: "ClientEntityId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Clients_ClientId",
                table: "WishListItems",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
