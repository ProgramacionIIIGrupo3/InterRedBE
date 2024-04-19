using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterRedBE.Migrations
{
    /// <inheritdoc />
    public partial class masnulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId",
                table: "Calificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_LugarTuristico_Departamento_DepartamentoId",
                table: "LugarTuristico");

            migrationBuilder.DropForeignKey(
                name: "FK_LugarTuristico_Municipio_MunicipioId",
                table: "LugarTuristico");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId",
                table: "Visita");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_IdCabecera",
                table: "Departamento");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Visita",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdLugarTuristico",
                table: "Visita",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdDepartamento",
                table: "Municipio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MunicipioId",
                table: "LugarTuristico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdMunicipio",
                table: "LugarTuristico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdDepartamento",
                table: "LugarTuristico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "LugarTuristico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdCabecera",
                table: "Departamento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Puntuacion",
                table: "Calificacion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Calificacion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdLugarTuristico",
                table: "Calificacion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_IdCabecera",
                table: "Departamento",
                column: "IdCabecera",
                unique: true,
                filter: "[IdCabecera] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId",
                table: "Calificacion",
                column: "LugarTuristicoId",
                principalTable: "LugarTuristico",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LugarTuristico_Departamento_DepartamentoId",
                table: "LugarTuristico",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LugarTuristico_Municipio_MunicipioId",
                table: "LugarTuristico",
                column: "MunicipioId",
                principalTable: "Municipio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio",
                column: "IdDepartamento",
                principalTable: "Departamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId",
                table: "Visita",
                column: "LugarTuristicoId",
                principalTable: "LugarTuristico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId",
                table: "Calificacion");

            migrationBuilder.DropForeignKey(
                name: "FK_LugarTuristico_Departamento_DepartamentoId",
                table: "LugarTuristico");

            migrationBuilder.DropForeignKey(
                name: "FK_LugarTuristico_Municipio_MunicipioId",
                table: "LugarTuristico");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId",
                table: "Visita");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_IdCabecera",
                table: "Departamento");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Visita",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdLugarTuristico",
                table: "Visita",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdDepartamento",
                table: "Municipio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MunicipioId",
                table: "LugarTuristico",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdMunicipio",
                table: "LugarTuristico",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdDepartamento",
                table: "LugarTuristico",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "LugarTuristico",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCabecera",
                table: "Departamento",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Puntuacion",
                table: "Calificacion",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Calificacion",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdLugarTuristico",
                table: "Calificacion",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_IdCabecera",
                table: "Departamento",
                column: "IdCabecera",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId",
                table: "Calificacion",
                column: "LugarTuristicoId",
                principalTable: "LugarTuristico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LugarTuristico_Departamento_DepartamentoId",
                table: "LugarTuristico",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LugarTuristico_Municipio_MunicipioId",
                table: "LugarTuristico",
                column: "MunicipioId",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio",
                column: "IdDepartamento",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId",
                table: "Visita",
                column: "LugarTuristicoId",
                principalTable: "LugarTuristico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
