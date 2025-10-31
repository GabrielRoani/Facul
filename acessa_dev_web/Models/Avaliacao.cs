using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acessa_dev_web.Models

{
    [Table("Avaliacoes")]
    public class Avaliacao

    {
        [Key]
        public int idAvaliacao { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        public string DescricaoAvaliacao { get; set; }

        [Required(ErrorMessage = "O valor da avalaição é obrigatório!")]
        public int ValorAvaliacao { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório!")]
        public DateTime Data { get; set; }

        [ForeignKey("idUsuario")]
        public int idUsuario { get; set; }

        [ForeignKey("idlocal")]
        public int idLocal { get; set; }
    }
}