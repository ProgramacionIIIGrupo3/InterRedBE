using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterRedBE.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyNamingAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId",
                table: "Visita");

            migrationBuilder.RenameColumn(
                name: "IdLugarTuristico",
                table: "Visita",
                newName: "LugarTuristicoId1");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Visita",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_LugarTuristicoId1",
                table: "Visita",
                column: "LugarTuristicoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId",
                table: "Visita",
                column: "LugarTuristicoId",
                principalTable: "LugarTuristico",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId1",
                table: "Visita",
                column: "LugarTuristicoId1",
                principalTable: "LugarTuristico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId",
                table: "Visita");

            migrationBuilder.DropForeignKey(
                name: "FK_Visita_LugarTuristico_LugarTuristicoId1",
                table: "Visita");

            migrationBuilder.DropIndex(
                name: "IX_Visita_LugarTuristicoId1",
                table: "Visita");

            migrationBuilder.RenameColumn(
                name: "LugarTuristicoId1",
                table: "Visita",
                newName: "IdLugarTuristico");

            migrationBuilder.AlterColumn<int>(
                name: "LugarTuristicoId",
                table: "Visita",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
