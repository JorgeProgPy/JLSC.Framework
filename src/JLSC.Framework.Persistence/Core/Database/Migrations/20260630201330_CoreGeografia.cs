using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Core.Migrations
{
    /// <inheritdoc />
    public partial class CoreGeografia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pais",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CodigoISO2 = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CodigoISO3 = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Nacionalidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CodigoTelefonico = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    DominioInternet = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaisId = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamento_Pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "core",
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartamentoId = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provincia_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalSchema: "core",
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProvinciaId = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudad_Provincia_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalSchema: "core",
                        principalTable: "Provincia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_ProvinciaId_Codigo",
                schema: "core",
                table: "Ciudad",
                columns: new[] { "ProvinciaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_PublicId",
                schema: "core",
                table: "Ciudad",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_PaisId_Codigo",
                schema: "core",
                table: "Departamento",
                columns: new[] { "PaisId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_PublicId",
                schema: "core",
                table: "Departamento",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pais_Codigo",
                schema: "core",
                table: "Pais",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pais_CodigoISO2",
                schema: "core",
                table: "Pais",
                column: "CodigoISO2",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pais_CodigoISO3",
                schema: "core",
                table: "Pais",
                column: "CodigoISO3",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pais_PublicId",
                schema: "core",
                table: "Pais",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provincia_DepartamentoId_Codigo",
                schema: "core",
                table: "Provincia",
                columns: new[] { "DepartamentoId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provincia_PublicId",
                schema: "core",
                table: "Provincia",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciudad",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Provincia",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Departamento",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Pais",
                schema: "core");
        }
    }
}
