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
    }
 }