using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterRedBE.Migrations
{
    /// <inheritdoc />
    public partial class municipioUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Municipio",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Municipio");
        }
    }
}
