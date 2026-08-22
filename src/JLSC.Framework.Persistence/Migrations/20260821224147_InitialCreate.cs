using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JLSC.Framework.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.EnsureSchema(
                name: "Seguridad");

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
                    PropietarioId = table.Column<long>(type: "bigint", nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false)
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
                name: "Catalogo",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EsSistema = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacto",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
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
                name: "Modulos",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Icono = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    Visible = table.Column<bool>(type: "boolean", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoISO2 = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CodigoISO3 = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Nacionalidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CodigoTelefonico = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    DominioInternet = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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
                name: "Roles",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                    Icono = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CssClass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UrlBase = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Target = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
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

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuloId = table.Column<long>(type: "bigint", nullable: false),
                    MenuPadreId = table.Column<long>(type: "bigint", nullable: true),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Ruta = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Icono = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    MostrarEnMenu = table.Column<bool>(type: "boolean", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_MenuPadreId",
                        column: x => x.MenuPadreId,
                        principalSchema: "Seguridad",
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menus_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "Seguridad",
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuloId = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permisos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "Seguridad",
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaisId = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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
                    Codigo = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoPersonaId = table.Column<long>(type: "bigint", nullable: false),
                    TipoDocumentoId = table.Column<long>(type: "bigint", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Complemento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Nit = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Nombres = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    PrimerApellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SegundoApellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    GeneroId = table.Column<long>(type: "bigint", nullable: true),
                    EstadoCivilId = table.Column<long>(type: "bigint", nullable: true),
                    RazonSocial = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    NombreComercial = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Sigla = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Observacion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItem_EstadoCivilId",
                        column: x => x.EstadoCivilId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItem_GeneroId",
                        column: x => x.GeneroId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItem_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_CatalogoItem_TipoPersonaId",
                        column: x => x.TipoPersonaId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolId = table.Column<long>(type: "bigint", nullable: false),
                    PermisoId = table.Column<long>(type: "bigint", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalSchema: "Seguridad",
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles_RolId",
                        column: x => x.RolId,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
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
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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
                name: "Archivo",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    PropietarioPersonaId = table.Column<long>(type: "bigint", nullable: true),
                    ArchivoAnteriorId = table.Column<long>(type: "bigint", nullable: true),
                    EsPublico = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Archivo_Personas_PropietarioPersonaId",
                        column: x => x.PropietarioPersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PersonaId = table.Column<long>(type: "bigint", nullable: false),
                    NombreUsuario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UltimoAcceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DebeCambiarPassword = table.Column<bool>(type: "boolean", nullable: false),
                    Bloqueado = table.Column<bool>(type: "boolean", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
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
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "archivo_referencias",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArchivoId = table.Column<long>(type: "bigint", nullable: false),
                    ModuloId = table.Column<long>(type: "bigint", nullable: false),
                    EntidadId = table.Column<long>(type: "bigint", nullable: false),
                    TipoUsoId = table.Column<long>(type: "bigint", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Observacion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivo_referencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_archivo_referencias_Archivo_ArchivoId",
                        column: x => x.ArchivoId,
                        principalSchema: "core",
                        principalTable: "Archivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_archivo_referencias_CatalogoItem_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_archivo_referencias_CatalogoItem_TipoUsoId",
                        column: x => x.TipoUsoId,
                        principalSchema: "core",
                        principalTable: "CatalogoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRoles",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    RolId = table.Column<long>(type: "bigint", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioCreacionId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioModificacionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalSchema: "Seguridad",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    NumeroPuerta = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Edificio = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Piso = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Oficina = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CodigoPostal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Referencia = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Latitud = table.Column<double>(type: "double precision", nullable: true),
                    Longitud = table.Column<double>(type: "double precision", nullable: true),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
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
                name: "IX_Archivo_PropietarioPersonaId",
                schema: "core",
                table: "Archivo",
                column: "PropietarioPersonaId");

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
                name: "IX_archivo_referencias_ArchivoId",
                schema: "core",
                table: "archivo_referencias",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_archivo_referencias_ModuloId_EntidadId",
                schema: "core",
                table: "archivo_referencias",
                columns: new[] { "ModuloId", "EntidadId" });

            migrationBuilder.CreateIndex(
                name: "IX_archivo_referencias_ModuloId_EntidadId_Activo",
                schema: "core",
                table: "archivo_referencias",
                columns: new[] { "ModuloId", "EntidadId", "Activo" });

            migrationBuilder.CreateIndex(
                name: "IX_archivo_referencias_ModuloId_EntidadId_EsPrincipal",
                schema: "core",
                table: "archivo_referencias",
                columns: new[] { "ModuloId", "EntidadId", "EsPrincipal" });

            migrationBuilder.CreateIndex(
                name: "IX_archivo_referencias_ModuloId_EntidadId_TipoUsoId",
                schema: "core",
                table: "archivo_referencias",
                columns: new[] { "ModuloId", "EntidadId", "TipoUsoId" });

            migrationBuilder.CreateIndex(
                name: "IX_archivo_referencias_TipoUsoId",
                schema: "core",
                table: "archivo_referencias",
                column: "TipoUsoId");

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
                name: "IX_Menus_Codigo",
                schema: "Seguridad",
                table: "Menus",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuPadreId",
                schema: "Seguridad",
                table: "Menus",
                column: "MenuPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ModuloId",
                schema: "Seguridad",
                table: "Menus",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Nombre",
                schema: "Seguridad",
                table: "Menus",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Orden",
                schema: "Seguridad",
                table: "Menus",
                column: "Orden");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_PublicId",
                schema: "Seguridad",
                table: "Menus",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Codigo",
                schema: "Seguridad",
                table: "Modulos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Nombre",
                schema: "Seguridad",
                table: "Modulos",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_Orden",
                schema: "Seguridad",
                table: "Modulos",
                column: "Orden");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_PublicId",
                schema: "Seguridad",
                table: "Modulos",
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
                name: "IX_Permisos_Codigo",
                schema: "Seguridad",
                table: "Permisos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_ModuloId",
                schema: "Seguridad",
                table: "Permisos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Nombre",
                schema: "Seguridad",
                table: "Permisos",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Orden",
                schema: "Seguridad",
                table: "Permisos",
                column: "Orden");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_PublicId",
                schema: "Seguridad",
                table: "Permisos",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Codigo",
                table: "Personas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_EstadoCivilId",
                table: "Personas",
                column: "EstadoCivilId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_GeneroId",
                table: "Personas",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Nit",
                table: "Personas",
                column: "Nit");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_PublicId",
                table: "Personas",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TipoDocumentoId_NumeroDocumento_Complemento",
                table: "Personas",
                columns: new[] { "TipoDocumentoId", "NumeroDocumento", "Complemento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TipoPersonaId",
                table: "Personas",
                column: "TipoPersonaId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Codigo",
                schema: "Seguridad",
                table: "Roles",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Nombre",
                schema: "Seguridad",
                table: "Roles",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_PublicId",
                schema: "Seguridad",
                table: "Roles",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_Codigo",
                schema: "Seguridad",
                table: "RolPermisos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PermisoId",
                schema: "Seguridad",
                table: "RolPermisos",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PublicId",
                schema: "Seguridad",
                table: "RolPermisos",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_RolId",
                schema: "Seguridad",
                table: "RolPermisos",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_RolId_PermisoId",
                schema: "Seguridad",
                table: "RolPermisos",
                columns: new[] { "RolId", "PermisoId" },
                unique: true);

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
                name: "IX_Ubicacion_PaisId_DepartamentoId_ProvinciaId_CiudadId",
                schema: "core",
                table: "Ubicacion",
                columns: new[] { "PaisId", "DepartamentoId", "ProvinciaId", "CiudadId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_Codigo",
                schema: "Seguridad",
                table: "UsuarioRoles",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_PublicId",
                schema: "Seguridad",
                table: "UsuarioRoles",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_RolId",
                schema: "Seguridad",
                table: "UsuarioRoles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_UsuarioId",
                schema: "Seguridad",
                table: "UsuarioRoles",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_UsuarioId_RolId",
                schema: "Seguridad",
                table: "UsuarioRoles",
                columns: new[] { "UsuarioId", "RolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Codigo",
                schema: "Seguridad",
                table: "Usuarios",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                schema: "Seguridad",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaId",
                schema: "Seguridad",
                table: "Usuarios",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PublicId",
                schema: "Seguridad",
                table: "Usuarios",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivo_referencias",
                schema: "core");

            migrationBuilder.DropTable(
                name: "ContactoItems");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "RolPermisos",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Ubicacion",
                schema: "core");

            migrationBuilder.DropTable(
                name: "UsuarioRoles",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Archivo",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Contacto",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Permisos",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Ciudad",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "CarpetaArchivo",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Modulos",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Provincia",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Departamento",
                schema: "core");

            migrationBuilder.DropTable(
                name: "CatalogoItem",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Pais",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Catalogo",
                schema: "core");
        }
    }
}
