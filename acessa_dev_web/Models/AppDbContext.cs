using Microsoft.EntityFrameworkCore;

namespace acessa_dev_web.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<MediaAvaliacao> MediaAvaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed de Usuários
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    id = 1,
                    Nome = "Usuário Demo",
                    Senha = "123456",
                    Perfil = Perfil.Cidadao
                },
                new Usuario
                {
                    id = 2,
                    Nome = "Administrador",
                    Senha = "admin123",
                    Perfil = Perfil.Admin
                }
            );

            // Seed de Locais
            modelBuilder.Entity<Local>().HasData(
                new Local
                {
                    idLocal = 1,
                    Nome = "Praça da Sé",
                    Endereco = "Praça da Sé, Centro, São Paulo - SP",
                    Latitude = -23.5505f,
                    Longitude = -46.6333f
                },
                new Local
                {
                    idLocal = 2,
                    Nome = "Avenida Paulista",
                    Endereco = "Avenida Paulista, 1000, São Paulo - SP",
                    Latitude = -23.5631f,
                    Longitude = -46.6540f
                },
                new Local
                {
                    idLocal = 3,
                    Nome = "Shopping Center Norte",
                    Endereco = "Shopping Center Norte, Tucuruvi, São Paulo - SP",
                    Latitude = -23.4896f,
                    Longitude = -46.6104f
                },
                new Local
                {
                    idLocal = 4,
                    Nome = "Parque Ibirapuera",
                    Endereco = "Parque Ibirapuera, Moema, São Paulo - SP",
                    Latitude = -23.5875f,
                    Longitude = -46.6576f
                },
                new Local
                {
                    idLocal = 5,
                    Nome = "Estação da Luz",
                    Endereco = "Estação da Luz, Centro, São Paulo - SP",
                    Latitude = -23.5350f,
                    Longitude = -46.6340f
                }
            );
        }
    }
}