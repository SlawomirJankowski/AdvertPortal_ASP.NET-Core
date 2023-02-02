using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDataTypeForPriceInOfferModelFromDoubleToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_ImagesCollections_ImagesCollectionId",
                table: "Offers");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Offers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "ImagesCollectionId",
                table: "Offers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_ImagesCollections_ImagesCollectionId",
                table: "Offers",
                column: "ImagesCollectionId",
                principalTable: "ImagesCollections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_ImagesCollections_ImagesCollectionId",
                table: "Offers");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Offers",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ImagesCollectionId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_ImagesCollections_ImagesCollectionId",
                table: "Offers",
                column: "ImagesCollectionId",
                principalTable: "ImagesCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
