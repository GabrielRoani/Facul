using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acessa_dev_web.Models

{
    [Table("Locais")]
    public class Local

    {
        [Key]
        public int idLocal { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório!")]
        public String Endereco { get; set; }

        [Required(ErrorMessage = "O campo Latitude é obrigatório!")]
        public float Latitude { get; set; }

        [Required(ErrorMessage = "O campo Longitude é obrigatório!")]
        public float Longitude { get; set; }

        public ICollection<Ocorrencia> Ocorrencias { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }

    }
}