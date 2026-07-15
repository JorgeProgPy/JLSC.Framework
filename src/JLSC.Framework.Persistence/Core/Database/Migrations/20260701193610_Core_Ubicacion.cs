using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JLSC.Framework.Persistence.Core.Migrations
{
    /// <inheritdoc />
    public partial class Core_Ubicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ubicacion_PaisId",
                schema: "core",
                table: "Ubicacion");

            migrationBuilder.RenameColumn(
                name: "Numero",
                schema: "core",
                table: "Ubicacion",
                newName: "NumeroPuerta");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                schema: "core",
                table: "Ubicacion",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_PaisId_DepartamentoId_ProvinciaId_CiudadId",
                schema: "core",
                table: "Ubicacion",
                columns: new[] { "PaisId", "DepartamentoId", "ProvinciaId", "CiudadId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ubicacion_PaisId_DepartamentoId_ProvinciaId_CiudadId",
                schema: "core",
                table: "Ubicacion");

            migrationBuilder.RenameColumn(
                name: "NumeroPuerta",
                schema: "core",
                table: "Ubicacion",
                newName: "Numero");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                schema: "core",
                table: "Ubicacion",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_PaisId",
                schema: "core",
                table: "Ubicacion",
                column: "PaisId");
        }
    }
}
