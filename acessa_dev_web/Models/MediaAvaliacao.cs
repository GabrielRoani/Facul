using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acessa_dev_web.Models
{
    [Table("MediaAvaliacoes")]
    public class MediaAvaliacao
    {
        [Key]
        public int idMediaAvaliacao { get; set; }

        public int idLocal { get; set; }

        [ForeignKey("idLocal")]
        public Local Local { get; set; }

        public int QtdAvaliacoes { get; set; }

        public float VlUltimaAvaliacao { get; set; }

        public float VlUltimaAvaliacaoMedia { get; set; }

        public float AvaliacaoMedia { get; set; }
    }
}