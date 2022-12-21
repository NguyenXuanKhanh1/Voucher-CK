using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoucherCK.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialDV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarCodeRedeems",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Barcode = table.Column<string>(type: "text", nullable: false),
                    Voucher = table.Column<string>(type: "text", nullable: false),
                    RedeemAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarCodeRedeems", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarCodeRedeems");
        }
    }
}
