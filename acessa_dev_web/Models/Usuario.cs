using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acessa_dev_web.Models
    
{
    [Table("Usuarios")]
    public class Usuario

    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Por favor, insira um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo Perfil é obrigatório!")]
        public Perfil Perfil { get; set; }

       // public ICollection<Ocorrencia> Ocorrencias { get; set; }
       // public ICollection<Avaliacao> Avaliacoes { get; set; }
    }

    public enum Perfil
    {
        Cidadao,
        Gestor,
        Admin
    }
}