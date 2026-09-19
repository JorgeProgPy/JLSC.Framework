using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatePortalBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Portal");

            migrationBuilder.CreateTable(
                name: "Banner",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Subtitulo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ArchivoId = table.Column<long>(type: "bigint", nullable: true),
                    Enlace = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banner_Archivo_ArchivoId",
                        column: x => x.ArchivoId,
                        principalSchema: "core",
                        principalTable: "Archivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banner_Activo_Orden",
                schema: "Portal",
                table: "Banner",
                columns: new[] { "Activo", "Orden" });

            migrationBuilder.CreateIndex(
                name: "IX_Banner_ArchivoId",
                schema: "Portal",
                table: "Banner",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_FechaInicio_FechaFin",
                schema: "Portal",
                table: "Banner",
                columns: new[] { "FechaInicio", "FechaFin" });

            migrationBuilder.CreateIndex(
                name: "IX_Banner_PublicId",
                schema: "Portal",
                table: "Banner",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner",
                schema: "Portal");
        }
    }
}
