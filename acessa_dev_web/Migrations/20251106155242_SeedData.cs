using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace acessa_dev_web.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locais",
                columns: new[] { "idLocal", "Endereco", "Latitude", "Longitude", "Nome" },
                values: new object[,]
                {
                    { 1, "Praça da Sé, Centro, São Paulo - SP", -23.5505f, -46.6333f, "Praça da Sé" },
                    { 2, "Avenida Paulista, 1000, São Paulo - SP", -23.5631f, -46.654f, "Avenida Paulista" },
                    { 3, "Shopping Center Norte, Tucuruvi, São Paulo - SP", -23.4896f, -46.6104f, "Shopping Center Norte" },
                    { 4, "Parque Ibirapuera, Moema, São Paulo - SP", -23.5875f, -46.6576f, "Parque Ibirapuera" },
                    { 5, "Estação da Luz, Centro, São Paulo - SP", -23.535f, -46.634f, "Estação da Luz" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id", "Nome", "Perfil", "Senha" },
                values: new object[,]
                {
                    { 1, "Usuário Demo", 0, "123456" },
                    { 2, "Administrador", 2, "admin123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "idLocal",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "idLocal",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "idLocal",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "idLocal",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "idLocal",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
