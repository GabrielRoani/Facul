using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace acessa_dev_web.Models

{
    [Table("Ocorrencias")]
    public class Ocorrencia

    {
        [Key]
        public int idOcorrencia { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        public string DescricaoOcorrencia { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório!")]        
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "O campo Severidade é obrigatório!")]
        public Severidade Severidade{ get; set; }

        [Required(ErrorMessage = "O campo Status é obrigatório!")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório!")]
        public DateTime Data { get; set; }
        
        [ForeignKey("idUsuario")]
        public int idUsuario { get; set; }

        [ForeignKey("idlocal")]
        public int idLocal { get; set; }
    }

    public enum Categoria
        {
            [Display(Name = "Iluminação")]
            Iluminacao,

            [Display(Name = "Pavimentação Tátil")]
            Pavimentacao,

            [Display(Name = "Sanitários como Acessibilidade")]
            Sanitarios,

            [Display(Name = "Rampas de Acesso")]
            Transporte,

            [Display(Name = "Calçadas rebaixadas")]
            Calcada,

            [Display(Name = "Calçadas rebaixadas")]    
            Elevadores
    }

public enum Severidade
    {
        Alta,
        Média,
        Baixa
    }
    public enum Status
    {
        Funcional,

        [Display(Name = "Não Funcional")]
        NaoFuncional,

        Inexistente
    }
}