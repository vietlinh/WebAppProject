using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppProject.Migrations
{
    /// <inheritdoc />
    public partial class UP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basicMeals_AspNetUsers_Creator_id",
                table: "basicMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_mainMeals_AspNetUsers_Creator_id",
                table: "mainMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_sideMeals_AspNetUsers_Creator_id",
                table: "sideMeals");

            migrationBuilder.AlterColumn<string>(
                name: "Creator_id",
                table: "sideMeals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "week_create",
                table: "sideMeals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "week_register",
                table: "registerMealInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Creator_id",
                table: "mainMeals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "week_create",
                table: "mainMeals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Creator_id",
                table: "basicMeals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "week_create",
                table: "basicMeals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_basicMeals_AspNetUsers_Creator_id",
                table: "basicMeals",
                column: "Creator_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mainMeals_AspNetUsers_Creator_id",
                table: "mainMeals",
                column: "Creator_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sideMeals_AspNetUsers_Creator_id",
                table: "sideMeals",
                column: "Creator_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_basicMeals_AspNetUsers_Creator_id",
                table: "basicMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_mainMeals_AspNetUsers_Creator_id",
                table: "mainMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_sideMeals_AspNetUsers_Creator_id",
                table: "sideMeals");

            migrationBuilder.DropColumn(
                name: "week_create",
                table: "sideMeals");

            migrationBuilder.DropColumn(
                name: "week_register",
                table: "registerMealInfos");

            migrationBuilder.DropColumn(
                name: "week_create",
                table: "mainMeals");

            migrationBuilder.DropColumn(
                name: "week_create",
                table: "basicMeals");

            migrationBuilder.AlterColumn<string>(
                name: "Creator_id",
                table: "sideMeals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Creator_id",
                table: "mainMeals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Creator_id",
                table: "basicMeals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_basicMeals_AspNetUsers_Creator_id",
                table: "basicMeals",
                column: "Creator_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mainMeals_AspNetUsers_Creator_id",
                table: "mainMeals",
                column: "Creator_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sideMeals_AspNetUsers_Creator_id",
                table: "sideMeals",
                column: "Creator_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
