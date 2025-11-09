using acessa_dev_web.Models;
using System.Collections.Generic;

namespace acessa_dev_web.ViewModels
{
    public class UserStatsViewModel
    {
        public int TotalOcorrencias { get; set; }
        public int TotalAvaliacoes { get; set; }
        public int OcorrenciasResolvidas { get; set; }
    }

    public class HomeDashboardViewModel
    {
        public UserStatsViewModel Stats { get; set; }
        public Ocorrencia NovaOcorrencia { get; set; }
        public Avaliacao NovaAvaliacao { get; set; }

        public IEnumerable<Local> TodosOsLocais { get; set; }
    }
}