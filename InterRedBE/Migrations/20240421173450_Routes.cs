using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterRedBE.Migrations
{
    /// <inheritdoc />
    public partial class Routes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDepartamentoInicio = table.Column<int>(type: "int", nullable: false),
                    IdDepartamentoFin = table.Column<int>(type: "int", nullable: false),
                    Distancia = table.Column<double>(type: "float", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ruta_Departamento_IdDepartamentoFin",
                        column: x => x.IdDepartamentoFin,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ruta_Departamento_IdDepartamentoInicio",
                        column: x => x.IdDepartamentoInicio,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ruta_IdDepartamentoFin",
                table: "Ruta",
                column: "IdDepartamentoFin");

            migrationBuilder.CreateIndex(
                name: "IX_Ruta_IdDepartamentoInicio",
                table: "Ruta",
                column: "IdDepartamentoInicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ruta");
        }
    }
}
