using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechDataInput.Migrations
{
    /// <inheritdoc />
    public partial class ValuesPaging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageNumber",
                table: "ParameterDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageNumber",
                table: "ParameterDefinitions");
        }
    }
}
