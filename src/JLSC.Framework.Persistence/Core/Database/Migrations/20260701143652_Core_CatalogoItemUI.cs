using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Core.Migrations
{
    /// <inheritdoc />
    public partial class Core_CatalogoItemUI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Orden",
                schema: "core",
                table: "CatalogoItem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<string>(
                name: "CssClass",
                schema: "core",
                table: "CatalogoItem",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                schema: "core",
                table: "CatalogoItem",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlBase",
                schema: "core",
                table: "CatalogoItem",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ubicacion",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaisId = table.Column<long>(type: "bigint", nullable: false),
                    DepartamentoId = table.Column<long>(type: "bigint", nullable: false),
                    ProvinciaId = table.Column<long>(type: "bigint", nullable: true),
                    CiudadId = table.Column<long>(type: "bigint", nullable: true),
                    Urbanizacion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Barrio = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Zona = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Avenida = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Calle = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Numero = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Edificio = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Piso = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Oficina = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CodigoPostal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Referencia = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Latitud = table.Column<double>(type: "double precision", nullable: true),
                    Longitud = table.Column<double>(type: "double precision", nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Ciudad_CiudadId",
                        column: x => x.CiudadId,
                        principalSchema: "core",
                        principalTable: "Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalSchema: "core",
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "core",
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Provincia_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalSchema: "core",
                        principalTable: "Provincia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_CiudadId",
                schema: "core",
                table: "Ubicacion",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_DepartamentoId",
                schema: "core",
                table: "Ubicacion",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_PaisId",
                schema: "core",
                table: "Ubicacion",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_ProvinciaId",
                schema: "core",
                table: "Ubicacion",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_PublicId",
                schema: "core",
                table: "Ubicacion",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ubicacion",
                schema: "core");

            migrationBuilder.DropColumn(
                name: "CssClass",
                schema: "core",
                table: "CatalogoItem");

            migrationBuilder.DropColumn(
                name: "Target",
                schema: "core",
                table: "CatalogoItem");

            migrationBuilder.DropColumn(
                name: "UrlBase",
                schema: "core",
                table: "CatalogoItem");

            migrationBuilder.AlterColumn<short>(
                name: "Orden",
                schema: "core",
                table: "CatalogoItem",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);
        }
    }
}
