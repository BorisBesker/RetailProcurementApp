using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuplierStoreItem",
                columns: table => new
                {
                    StoreItemId = table.Column<int>(type: "int", nullable: false),
                    SuplierId = table.Column<int>(type: "int", nullable: false),
                    SuplierPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuplierStoreItem", x => new { x.StoreItemId, x.SuplierId });
                    table.ForeignKey(
                        name: "FK_SuplierStoreItem_StoreItems_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "StoreItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuplierStoreItem_Supliers_SuplierId",
                        column: x => x.SuplierId,
                        principalTable: "Supliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StoreItems",
                columns: new[] { "Id", "ItemDescription", "ItemName", "Price" },
                values: new object[,]
                {
                    { 1, "desc1", "item1", 0m },
                    { 2, "desc2", "item2", 0m },
                    { 3, "desc3", "item3", 0m },
                    { 4, "desc4", "item4", 0m },
                    { 5, "desc5", "item5", 0m },
                    { 6, "desc6", "item6", 0m },
                    { 7, "desc7", "item7", 0m },
                    { 8, "desc8", "item8", 0m },
                    { 9, "desc9", "item9", 0m },
                    { 10, "desc10", "item10", 0m }
                });

            migrationBuilder.InsertData(
                table: "Supliers",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "desc1", "Germany", "L-SHOP-TEAM GmbH" },
                    { 2, "desc2", "Netherlands", "Cotton Classic Handels GmbH" },
                    { 3, "desc3", "Poland", "UTT Europe Kft." },
                    { 4, "desc4", "Czech Republic", "SIPEC SpA" },
                    { 5, "desc5", "Greece", "MAXIM Ceramics Sp. z o. o. Sp. k." },
                    { 6, "desc6", "Spain", "TROIKA Germany GmbH" },
                    { 7, "desc7", "Spain", "Halfar System GmbH" },
                    { 8, "desc8", "Poland", "Araco International B.V." },
                    { 9, "desc9", "Germany", "Clipper B.V." },
                    { 10, "desc10", "Poland", "Toppoint B.V." }
                });

            migrationBuilder.InsertData(
                table: "SuplierStoreItem",
                columns: new[] { "StoreItemId", "SuplierId", "SuplierPrice" },
                values: new object[,]
                {
                    { 1, 1, 10m },
                    { 1, 2, 20m },
                    { 2, 1, 30m },
                    { 2, 4, 40m },
                    { 2, 5, 55m },
                    { 4, 6, 22m },
                    { 5, 7, 22m },
                    { 6, 1, 70m },
                    { 7, 4, 100m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuplierStoreItem_SuplierId",
                table: "SuplierStoreItem",
                column: "SuplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuplierStoreItem");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StoreItems");

            migrationBuilder.DropTable(
                name: "Supliers");
        }
    }
}
