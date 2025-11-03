using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace acessa_dev_web.Migrations
{
    /// <inheritdoc />
    public partial class M02AddTabelasOcor_Aval_Locais_MediaAval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    idLocal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.idLocal);
                });

            migrationBuilder.CreateTable(
                name: "MediaAvaliacoes",
                columns: table => new
                {
                    idMediaAvaliacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idLocal = table.Column<int>(type: "int", nullable: false),
                    QtdAvaliacoes = table.Column<int>(type: "int", nullable: false),
                    VlUltimaAvaliacao = table.Column<float>(type: "real", nullable: false),
                    AvaliacaoMedia = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAvaliacoes", x => x.idMediaAvaliacao);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    idAvaliacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoAvaliacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorAvaliacao = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idLocal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.idAvaliacao);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Locais_idLocal",
                        column: x => x.idLocal,
                        principalTable: "Locais",
                        principalColumn: "idLocal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    idOcorrencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoOcorrencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Severidade = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idLocal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.idOcorrencia);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Locais_idLocal",
                        column: x => x.idLocal,
                        principalTable: "Locais",
                        principalColumn: "idLocal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_idLocal",
                table: "Avaliacoes",
                column: "idLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_idUsuario",
                table: "Avaliacoes",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_idLocal",
                table: "Ocorrencias",
                column: "idLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_idUsuario",
                table: "Ocorrencias",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "MediaAvaliacoes");

            migrationBuilder.DropTable(
                name: "Ocorrencias");

            migrationBuilder.DropTable(
                name: "Locais");
        }
    }
}
