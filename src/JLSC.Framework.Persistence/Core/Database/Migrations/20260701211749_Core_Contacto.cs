using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Core.Migrations
{
    /// <inheritdoc />
    public partial class Core_Contacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacto",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactoItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactoId = table.Column<long>(type: "bigint", nullable: false),
                    TipoContactoId = table.Column<long>(type: "bigint", nullable: false),
                    Valor = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    VisiblePortal = table.Column<bool>(type: "boolean", nullable: false),
                    VisibleDirectorio = table.Column<bool>(type: "boolean", nullable: false),
                    Verificado = table.Column<bool>(type: "boolean", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactoItems_CatalogoItem_TipoContactoId",
                        column: x => x.TipoContactoId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactoItems_Contacto_ContactoId",
                        column: x => x.ContactoId,
                        principalSchema: "core",
                        principalTable: "Contacto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacto_PublicId",
                schema: "core",
                table: "Contacto",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactoItems_ContactoId",
                table: "ContactoItems",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactoItems_TipoContactoId",
                table: "ContactoItems",
                column: "TipoContactoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactoItems");

            migrationBuilder.DropTable(
                name: "Contacto",
                schema: "core");
        }
    }
}
