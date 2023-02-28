using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class updatedTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Todos",
                table: "Todos");

            migrationBuilder.RenameTable(
                name: "Todos",
                newName: "Todo");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Todo",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Todo",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Todo",
                newName: "description");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todo",
                table: "Todo",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "description", "status", "title" },
                values: new object[,]
                {
                    { 1, "description1", 0, "title1" },
                    { 2, "description2", 0, "title2" },
                    { 3, "description3", 0, "title3" },
                    { 4, "description4", 0, "title4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Todo",
                table: "Todo");

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Todo",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameTable(
                name: "Todo",
                newName: "Todos");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Todos",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Todos",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Todos",
                newName: "Description");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todos",
                table: "Todos",
                column: "Id");
        }
    }
}
