using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pabp_projekat_web_api_entity.Migrations
{
    /// <inheritdoc />
    public partial class prijava : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prijava_brojIndeksa",
                columns: table => new
                {
                    IdStudenta = table.Column<int>(type: "int", nullable: false),
                    IdIspita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prijava_brojIndeksa", x => new { x.IdStudenta, x.IdIspita });
                    table.ForeignKey(
                        name: "FK_Prijava_brojIndeksa_ispit_IdIspita",
                        column: x => x.IdIspita,
                        principalTable: "ispit",
                        principalColumn: "ID_ISPITA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prijava_brojIndeksa_student_IdStudenta",
                        column: x => x.IdStudenta,
                        principalTable: "student",
                        principalColumn: "ID_STUDENTA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prijava_brojIndeksa_IdIspita",
                table: "Prijava_brojIndeksa",
                column: "IdIspita");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prijava_brojIndeksa");
        }
    }
}
