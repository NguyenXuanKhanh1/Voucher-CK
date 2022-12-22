using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoucherCK.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddContrainabc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BarCodeRedeems_Barcode",
                table: "BarCodeRedeems",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarCodeRedeems_Voucher",
                table: "BarCodeRedeems",
                column: "Voucher",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BarCodeRedeems_Barcode",
                table: "BarCodeRedeems");

            migrationBuilder.DropIndex(
                name: "IX_BarCodeRedeems_Voucher",
                table: "BarCodeRedeems");
        }
    }
}
