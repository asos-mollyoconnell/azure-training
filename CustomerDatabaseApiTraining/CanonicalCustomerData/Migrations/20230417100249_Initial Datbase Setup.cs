using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CanonicalCustomerData.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatbaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanonicalCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanonicalCustomers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CanonicalCustomers",
                columns: new[] { "Id", "Country", "CustomerId", "Email", "FullName", "MobileNumber" },
                values: new object[] { 1, "Ireland", 1, "email@email.com", "Molly OConnell", 1234567890 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanonicalCustomers");
        }
    }
}
