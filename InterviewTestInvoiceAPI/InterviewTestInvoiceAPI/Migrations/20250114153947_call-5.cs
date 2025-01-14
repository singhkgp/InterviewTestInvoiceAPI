using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTestInvoiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class call5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status2",
                table: "TestInvoice",
                newName: "Status3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status3",
                table: "TestInvoice",
                newName: "Status2");
        }
    }
}
