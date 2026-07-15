using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "Catalogo",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EsSistema = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogoItem",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatalogoId = table.Column<long>(type: "bigint", nullable: false),
                    Valor = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Icono = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogoItem_Catalogo_CatalogoId",
                        column: x => x.CatalogoId,
                        principalSchema: "core",
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalogo_Codigo",
                schema: "core",
                table: "Catalogo",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Catalogo_PublicId",
                schema: "core",
                table: "Catalogo",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoItem_CatalogoId_Codigo",
                schema: "core",
                table: "CatalogoItem",
                columns: new[] { "CatalogoId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoItem_PublicId",
                schema: "core",
                table: "CatalogoItem",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogoItem",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Catalogo",
                schema: "core");
        }
    }
}
