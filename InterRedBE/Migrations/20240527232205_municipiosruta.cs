using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterRedBE.Migrations
{
    /// <inheritdoc />
    public partial class municipiosruta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ruta_Departamento_IdDepartamentoFin",
                table: "Ruta");

            migrationBuilder.DropForeignKey(
                name: "FK_Ruta_Departamento_IdDepartamentoInicio",
                table: "Ruta");

            migrationBuilder.RenameColumn(
                name: "IdDepartamentoInicio",
                table: "Ruta",
                newName: "IdEntidadInicio");

            migrationBuilder.RenameColumn(
                name: "IdDepartamentoFin",
                table: "Ruta",
                newName: "IdEntidadFin");

            migrationBuilder.RenameIndex(
                name: "IX_Ruta_IdDepartamentoInicio",
                table: "Ruta",
                newName: "IX_Ruta_IdEntidadInicio");

            migrationBuilder.RenameIndex(
                name: "IX_Ruta_IdDepartamentoFin",
                table: "Ruta",
                newName: "IX_Ruta_IdEntidadFin");

            migrationBuilder.AddColumn<string>(
                name: "TipoFin",
                table: "Ruta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoInicio",
                table: "Ruta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Ruta_Departamento_IdEntidadFin",
                table: "Ruta",
                column: "IdEntidadFin",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ruta_Departamento_IdEntidadInicio",
                table: "Ruta",
                column: "IdEntidadInicio",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ruta_Municipio_IdEntidadFin",
                table: "Ruta",
                column: "IdEntidadFin",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ruta_Municipio_IdEntidadInicio",
                table: "Ruta",
                column: "IdEntidadInicio",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ruta_Departamento_IdEntidadFin",
                table: "Ruta");

            migrationBuilder.DropForeignKey(
                name: "FK_Ruta_Departamento_IdEntidadInicio",
                table: "Ruta");

            migrationBuilder.DropForeignKey(
                name: "FK_Ruta_Municipio_IdEntidadFin",
                table: "Ruta");

            migrationBuilder.DropForeignKey(
                name: "FK_Ruta_Municipio_IdEntidadInicio",
                table: "Ruta");

            migrationBuilder.DropColumn(
                name: "TipoFin",
                table: "Ruta");

            migrationBuilder.DropColumn(
                name: "TipoInicio",
                table: "Ruta");

            migrationBuilder.RenameColumn(
                name: "IdEntidadInicio",
                table: "Ruta",
                newName: "IdDepartamentoInicio");

            migrationBuilder.RenameColumn(
                name: "IdEntidadFin",
                table: "Ruta",
                newName: "IdDepartamentoFin");

            migrationBuilder.RenameIndex(
                name: "IX_Ruta_IdEntidadInicio",
                table: "Ruta",
                newName: "IX_Ruta_IdDepartamentoInicio");

            migrationBuilder.RenameIndex(
                name: "IX_Ruta_IdEntidadFin",
                table: "Ruta",
                newName: "IX_Ruta_IdDepartamentoFin");

            migrationBuilder.AddForeignKey(
                name: "FK_Ruta_Departamento_IdDepartamentoFin",
                table: "Ruta",
                column: "IdDepartamentoFin",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ruta_Departamento_IdDepartamentoInicio",
                table: "Ruta",
                column: "IdDepartamentoInicio",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
