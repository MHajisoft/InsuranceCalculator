using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance.Service.Migrations
{
    /// <inheritdoc />
    public partial class Change_CreateUser_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceTypes_AspNetUsers_CreateUserId",
                table: "InsuranceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_AspNetUsers_CreateUserId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_CreateUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CreateUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CreateUserId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceTypes_CreateUserId",
                table: "InsuranceTypes");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "InsuranceTypes");

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Requests",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "Persons",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "InsuranceTypes",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "InsuranceTypes");

            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "Requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "Persons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreateUserId",
                table: "InsuranceTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CreateUserId",
                table: "Requests",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CreateUserId",
                table: "Persons",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceTypes_CreateUserId",
                table: "InsuranceTypes",
                column: "CreateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceTypes_AspNetUsers_CreateUserId",
                table: "InsuranceTypes",
                column: "CreateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_AspNetUsers_CreateUserId",
                table: "Persons",
                column: "CreateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_CreateUserId",
                table: "Requests",
                column: "CreateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
