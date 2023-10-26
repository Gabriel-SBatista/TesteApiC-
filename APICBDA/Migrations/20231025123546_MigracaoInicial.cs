using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICBDA.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estilos",
                columns: table => new
                {
                    EstiloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estilos", x => x.EstiloId);
                });

            migrationBuilder.CreateTable(
                name: "Sexos",
                columns: table => new
                {
                    SexoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexos", x => x.SexoId);
                });

            migrationBuilder.CreateTable(
                name: "Provas",
                columns: table => new
                {
                    ProvaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distancia = table.Column<int>(type: "int", nullable: false),
                    EstiloId = table.Column<int>(type: "int", nullable: false),
                    SexoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provas", x => x.ProvaId);
                    table.ForeignKey(
                        name: "FK_Provas_Estilos_EstiloId",
                        column: x => x.EstiloId,
                        principalTable: "Estilos",
                        principalColumn: "EstiloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Provas_Sexos_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexos",
                        principalColumn: "SexoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provas_EstiloId",
                table: "Provas",
                column: "EstiloId");

            migrationBuilder.CreateIndex(
                name: "IX_Provas_SexoId",
                table: "Provas",
                column: "SexoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provas");

            migrationBuilder.DropTable(
                name: "Estilos");

            migrationBuilder.DropTable(
                name: "Sexos");
        }
    }
}
