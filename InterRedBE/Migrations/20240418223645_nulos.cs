using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterRedBE.Migrations
{
    /// <inheritdoc />
    public partial class nulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_IdCabecera",
                table: "Departamento");

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
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio",
                column: "IdDepartamento",
                principalTable: "Departamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_IdCabecera",
                table: "Departamento");

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
                name: "FK_Municipio_Departamento_IdDepartamento",
                table: "Municipio",
                column: "IdDepartamento",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
