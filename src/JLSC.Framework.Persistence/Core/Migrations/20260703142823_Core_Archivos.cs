using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Core.Migrations
{
    /// <inheritdoc />
    public partial class Core_Archivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarpetaArchivo",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarpetaPadreId = table.Column<long>(type: "bigint", nullable: true),
                    Icono = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CssClass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EsRaiz = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Visible = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Orden = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarpetaArchivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarpetaArchivo_CarpetaArchivo_CarpetaPadreId",
                        column: x => x.CarpetaPadreId,
                        principalSchema: "core",
                        principalTable: "CarpetaArchivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Archivo",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    NombreOriginal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NombreAlmacenado = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CarpetaArchivoId = table.Column<long>(type: "bigint", nullable: false),
                    ClaveAlmacenamiento = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Extension = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    TamanoBytes = table.Column<long>(type: "bigint", nullable: false),
                    Hash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    TipoArchivoId = table.Column<long>(type: "bigint", nullable: false),
                    EstadoArchivoId = table.Column<long>(type: "bigint", nullable: false),
                    ProveedorAlmacenamientoId = table.Column<long>(type: "bigint", nullable: false),
                    ArchivoAnteriorId = table.Column<long>(type: "bigint", nullable: true),
                    EsPublico = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archivo_Archivo_ArchivoAnteriorId",
                        column: x => x.ArchivoAnteriorId,
                        principalSchema: "core",
                        principalTable: "Archivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Archivo_CarpetaArchivo_CarpetaArchivoId",
                        column: x => x.CarpetaArchivoId,
                        principalSchema: "core",
                        principalTable: "CarpetaArchivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Archivo_CatalogoItem_EstadoArchivoId",
                        column: x => x.EstadoArchivoId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Archivo_CatalogoItem_ProveedorAlmacenamientoId",
                        column: x => x.ProveedorAlmacenamientoId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Archivo_CatalogoItem_TipoArchivoId",
                        column: x => x.TipoArchivoId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_ArchivoAnteriorId",
                schema: "core",
                table: "Archivo",
                column: "ArchivoAnteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_CarpetaArchivoId",
                schema: "core",
                table: "Archivo",
                column: "CarpetaArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_ClaveAlmacenamiento",
                schema: "core",
                table: "Archivo",
                column: "ClaveAlmacenamiento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_Codigo",
                schema: "core",
                table: "Archivo",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_EstadoArchivoId",
                schema: "core",
                table: "Archivo",
                column: "EstadoArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_Hash",
                schema: "core",
                table: "Archivo",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_ProveedorAlmacenamientoId",
                schema: "core",
                table: "Archivo",
                column: "ProveedorAlmacenamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_PublicId",
                schema: "core",
                table: "Archivo",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_TipoArchivoId",
                schema: "core",
                table: "Archivo",
                column: "TipoArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarpetaArchivo_CarpetaPadreId_Codigo",
                schema: "core",
                table: "CarpetaArchivo",
                columns: new[] { "CarpetaPadreId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarpetaArchivo_PublicId",
                schema: "core",
                table: "CarpetaArchivo",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivo",
                schema: "core");

            migrationBuilder.DropTable(
                name: "CarpetaArchivo",
                schema: "core");
        }
    }
}
