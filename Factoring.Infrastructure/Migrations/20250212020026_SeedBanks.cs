using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoringAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedBanks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Name", "SWIFT" },
                values: new object[,]
                {
                    { "Bank Pekao SA", "PKOPPLPW" },
                    { "PKO Bank Polski", "BPKOPLPW" },
                    { "Santander Bank Polska", "WBKPPLPP" },
                    { "ING Bank Śląski", "INGBPLPW" },
                    { "mBank", "BREXPLPW" },
                    { "Bank Millennium", "BIGBPLPW" },
                    { "Alior Bank", "ALBPPLPW" },
                    { "BNP Paribas Bank Polska", "PPABPLPK" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Banks WHERE SWIFT IN ('PKOPPLPW', 'BPKOPLPW', 'WBKPPLPP', 'INGBPLPW', 'BREXPLPW', 'BIGBPLPW', 'ALBPPLPW', 'PPABPLPK')");
        }
    }
}
