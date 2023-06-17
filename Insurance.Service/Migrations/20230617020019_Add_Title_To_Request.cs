using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance.Service.Migrations
{
    /// <inheritdoc />
    public partial class Add_Title_To_Request : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Requests");
        }
    }
}
