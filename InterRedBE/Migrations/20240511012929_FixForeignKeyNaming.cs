using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterRedBE.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId",
                table: "Calificacion");

            migrationBuilder.RenameColumn(
                name: "IdLugarTuristico",
                table: "Calificacion",
                newName: "LugarTuristicoId1");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Calificacion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_LugarTuristicoId1",
                table: "Calificacion",
                column: "LugarTuristicoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId",
                table: "Calificacion",
                column: "LugarTuristicoId",
                principalTable: "LugarTuristico",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId1",
                table: "Calificacion",
                column: "LugarTuristicoId1",
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
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId1",
                table: "Calificacion");

            migrationBuilder.DropIndex(
                name: "IX_Calificacion_LugarTuristicoId1",
                table: "Calificacion");

            migrationBuilder.RenameColumn(
                name: "LugarTuristicoId1",
                table: "Calificacion",
                newName: "IdLugarTuristico");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Calificacion",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificacion_LugarTuristico_LugarTuristicoId",
                table: "Calificacion",
                column: "LugarTuristicoId",
                principalTable: "LugarTuristico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
