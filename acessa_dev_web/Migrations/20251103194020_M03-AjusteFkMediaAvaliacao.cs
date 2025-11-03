using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace acessa_dev_web.Migrations
{
    /// <inheritdoc />
    public partial class M03AjusteFkMediaAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "VlUltimaAvaliacaoMedia",
                table: "MediaAvaliacoes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_MediaAvaliacoes_idLocal",
                table: "MediaAvaliacoes",
                column: "idLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaAvaliacoes_Locais_idLocal",
                table: "MediaAvaliacoes",
                column: "idLocal",
                principalTable: "Locais",
                principalColumn: "idLocal",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaAvaliacoes_Locais_idLocal",
                table: "MediaAvaliacoes");

            migrationBuilder.DropIndex(
                name: "IX_MediaAvaliacoes_idLocal",
                table: "MediaAvaliacoes");

            migrationBuilder.DropColumn(
                name: "VlUltimaAvaliacaoMedia",
                table: "MediaAvaliacoes");
        }
    }
}
