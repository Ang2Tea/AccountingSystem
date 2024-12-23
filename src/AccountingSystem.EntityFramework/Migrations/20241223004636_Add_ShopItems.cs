using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingSystem.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Add_ShopItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItem_Categories_CategoryId",
                table: "ShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionShopItem_ShopItem_ShopItemId",
                table: "TransactionShopItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem");

            migrationBuilder.RenameTable(
                name: "ShopItem",
                newName: "ShopItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItem_CategoryId",
                table: "ShopItems",
                newName: "IX_ShopItems_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopItems",
                table: "ShopItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Categories_CategoryId",
                table: "ShopItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionShopItem_ShopItems_ShopItemId",
                table: "TransactionShopItem",
                column: "ShopItemId",
                principalTable: "ShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Categories_CategoryId",
                table: "ShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionShopItem_ShopItems_ShopItemId",
                table: "TransactionShopItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopItems",
                table: "ShopItems");

            migrationBuilder.RenameTable(
                name: "ShopItems",
                newName: "ShopItem");

            migrationBuilder.RenameIndex(
                name: "IX_ShopItems_CategoryId",
                table: "ShopItem",
                newName: "IX_ShopItem_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopItem",
                table: "ShopItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItem_Categories_CategoryId",
                table: "ShopItem",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionShopItem_ShopItem_ShopItemId",
                table: "TransactionShopItem",
                column: "ShopItemId",
                principalTable: "ShopItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
