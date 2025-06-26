using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tienda.Microservicios.Autor.Api.Migrations
{
    /// <inheritdoc />
    public partial class despliegueBD_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutorLibros",
                columns: table => new
                {
                    AutorLibroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorLibroGuid = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorLibros", x => x.AutorLibroId);
                });

            migrationBuilder.CreateTable(
                name: "GradosAcademicos",
                columns: table => new
                {
                    GradoAcademicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentroAcademico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaGrado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorLibroId = table.Column<int>(type: "int", nullable: false),
                    GradoAcademicoGuid = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradosAcademicos", x => x.GradoAcademicoId);
                    table.ForeignKey(
                        name: "FK_GradosAcademicos_AutorLibros_AutorLibroId",
                        column: x => x.AutorLibroId,
                        principalTable: "AutorLibros",
                        principalColumn: "AutorLibroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradosAcademicos_AutorLibroId",
                table: "GradosAcademicos",
                column: "AutorLibroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradosAcademicos");

            migrationBuilder.DropTable(
                name: "AutorLibros");
        }
    }
}
