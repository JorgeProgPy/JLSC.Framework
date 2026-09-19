using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JLSC.Framework.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRolPermisoCodigoUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolPermisos_Codigo",
                schema: "Seguridad",
                table: "RolPermisos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_Codigo",
                schema: "Seguridad",
                table: "RolPermisos",
                column: "Codigo",
                unique: true);
        }
    }
}
