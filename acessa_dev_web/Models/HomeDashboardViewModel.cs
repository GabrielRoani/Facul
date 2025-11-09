using System; 
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace acessa_dev_web.Models
{

    public class AtividadeRecenteItemViewModel
    {
        public string Icone { get; set; }           
        public string Titulo { get; set; }          
        public string Descricao { get; set; }       
        public DateTime Data { get; set; }          
        public string TempoDecorrido { get; set; }  
        public string Status { get; set; }          
        public string StatusCssClass { get; set; }  
    }

    public class UserStatsViewModel
    {
        public int TotalOcorrencias { get; set; }
        public int TotalAvaliacoes { get; set; }
    }

    public class HomeDashboardViewModel
    {
        public UserStatsViewModel Stats { get; set; }
        public Ocorrencia NovaOcorrencia { get; set; }
        public Avaliacao NovaAvaliacao { get; set; }
        public IEnumerable<Local> TodosOsLocais { get; set; }

        public List<AtividadeRecenteItemViewModel> AtividadesRecentes { get; set; }

        public HomeDashboardViewModel()
        {
            Stats = new UserStatsViewModel();
            NovaOcorrencia = new Ocorrencia();
            NovaAvaliacao = new Avaliacao();
            TodosOsLocais = new List<Local>();

            AtividadesRecentes = new List<AtividadeRecenteItemViewModel>();
        }
    }
}
