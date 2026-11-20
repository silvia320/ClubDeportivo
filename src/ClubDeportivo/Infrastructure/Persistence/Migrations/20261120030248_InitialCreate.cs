using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClubDeportivo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clubes",
                columns: table => new
                {
                    club_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    deporte = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    ubicacion = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    fecha_fundacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubes", x => x.club_id);
                });

            migrationBuilder.CreateTable(
                name: "miembros",
                columns: table => new
                {
                    miembro_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    apellido = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    telefono = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_miembros", x => x.miembro_id);
                });

            migrationBuilder.CreateTable(
                name: "inscripciones",
                columns: table => new
                {
                    inscripcion_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    club_id = table.Column<int>(type: "integer", nullable: false),
                    miembro_id = table.Column<int>(type: "integer", nullable: false),
                    fecha_inscripcion = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscripciones", x => x.inscripcion_id);
                    table.ForeignKey(
                        name: "FK_inscripciones_clubes_club_id",
                        column: x => x.club_id,
                        principalTable: "clubes",
                        principalColumn: "club_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_inscripciones_miembros_miembro_id",
                        column: x => x.miembro_id,
                        principalTable: "miembros",
                        principalColumn: "miembro_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clubes_nombre",
                table: "clubes",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_club_id_miembro_id",
                table: "inscripciones",
                columns: new[] { "club_id", "miembro_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_miembro_id",
                table: "inscripciones",
                column: "miembro_id");

            migrationBuilder.CreateIndex(
                name: "IX_miembros_email",
                table: "miembros",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inscripciones");

            migrationBuilder.DropTable(
                name: "clubes");

            migrationBuilder.DropTable(
                name: "miembros");
        }
    }
}
