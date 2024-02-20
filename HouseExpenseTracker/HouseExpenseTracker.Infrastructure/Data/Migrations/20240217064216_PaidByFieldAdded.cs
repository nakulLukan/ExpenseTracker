using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseExpenseTracker.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaidByFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaidById",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Name" },
                values: new object[] { -1, "Self" });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaidById",
                table: "Expenses",
                column: "PaidById");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Persons_PaidById",
                table: "Expenses",
                column: "PaidById",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Persons_PaidById",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PaidById",
                table: "Expenses");

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DropColumn(
                name: "PaidById",
                table: "Expenses");
        }
    }
}
