using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acessa_dev_web.Models
{
    [Table("MediaAvaliacoes")]
    public class MediaAvaliacao
    {
        [Key]
        public int idMediaAvaliacao { get; set; }

        [ForeignKey("idLocal")]
        public int idLocal { get; set; }
        
        public int QtdAvaliacoes { get; set; }

        public float VlUltimaAvaliacao { get; set; }
        
        public float AvaliacaoMedia { get; set; }
    }
}