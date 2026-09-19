using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatePortalProyectoDestacado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProyectoDestacado",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Ubicacion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ArchivoId = table.Column<long>(type: "bigint", nullable: true),
                    Enlace = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoDestacado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectoDestacado_Archivo_ArchivoId",
                        column: x => x.ArchivoId,
                        principalSchema: "core",
                        principalTable: "Archivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoDestacado_Activo_Orden",
                schema: "Portal",
                table: "ProyectoDestacado",
                columns: new[] { "Activo", "Orden" });

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoDestacado_ArchivoId",
                schema: "Portal",
                table: "ProyectoDestacado",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoDestacado_PublicId",
                schema: "Portal",
                table: "ProyectoDestacado",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProyectoDestacado",
                schema: "Portal");
        }
    }
}
