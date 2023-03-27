using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRatesTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_СurrencyCode",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_СurrencyCode_Date",
                table: "ExchangeRates",
                columns: new[] { "СurrencyCode", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_СurrencyCode_Date",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_СurrencyCode",
                table: "ExchangeRates",
                column: "СurrencyCode");
        }
    }
}
