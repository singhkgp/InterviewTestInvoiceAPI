using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewTestInvoiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class call3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status2",
                table: "TestInvoice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status2",
                table: "TestInvoice",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
