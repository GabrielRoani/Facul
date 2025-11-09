using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using acessa_dev_web.Models; 
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

[Authorize] 
public class HomepageController : Controller
{
    private readonly AppDbContext _context;

    public HomepageController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Dashboard()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Challenge();
        }


        var ultimasOcorrencias = await _context.Ocorrencias
            .Where(o => o.idUsuario.ToString() == userId)
            .OrderByDescending(o => o.Data) 
            .Take(5)
            .Select(o => new AtividadeRecenteItemViewModel
            {
                Icone = "📍",
                Titulo = "Nova ocorrência reportada",
                Descricao = o.DescricaoOcorrencia, 
                Data = o.Data,
                Status = "Crítica",
                StatusCssClass = "critical"
            })
            .ToListAsync();

        var ultimasAvaliacoes = await _context.Avaliacoes
            .Include(a => a.Local)
            .Where(a => a.idUsuario.ToString() == userId)
            .OrderByDescending(a => a.Data) 
            .Take(5)
            .Select(a => new AtividadeRecenteItemViewModel
            {
                Icone = "⭐",
                Titulo = "Você avaliou um local",
                Descricao = $"{a.Local.Nome} - {a.ValorAvaliacao} estrelas",
                Data = a.Data,
                Status = "Positivo",
                StatusCssClass = "positive"
            })
            .ToListAsync();

        var atividadesFinais = ultimasOcorrencias
            .Concat(ultimasAvaliacoes)
            .OrderByDescending(atividade => atividade.Data)
            .Take(5)
            .ToList();

        foreach (var atividade in atividadesFinais)
        {
            atividade.TempoDecorrido = CalcularTempoDecorrido(atividade.Data);
        }


        var userStats = new UserStatsViewModel
        {
            TotalOcorrencias = await _context.Ocorrencias
                                             .CountAsync(o => o.idUsuario.ToString() == userId),
            TotalAvaliacoes = await _context.Avaliacoes
                                            .CountAsync(a => a.idUsuario.ToString() == userId)
        };

        var viewModel = new HomeDashboardViewModel
        {
            Stats = userStats,
            TodosOsLocais = await _context.Locais.OrderBy(l => l.Nome).ToListAsync(),
            AtividadesRecentes = atividadesFinais 
        };

        return View(viewModel);
    }

    private string CalcularTempoDecorrido(DateTime data)
    {
        var diferenca = DateTime.Now - data;

        if (diferenca.TotalDays >= 2) return $"Há {(int)diferenca.TotalDays} dias";
        if (diferenca.TotalDays >= 1) return "Há 1 dia";
        if (diferenca.TotalHours >= 2) return $"Há {(int)diferenca.TotalHours} horas";
        if (diferenca.TotalHours >= 1) return "Há 1 hora";
        if (diferenca.TotalMinutes >= 2) return $"Há {(int)diferenca.TotalMinutes} minutos";
        if (diferenca.TotalMinutes >= 1) return "Há 1 minuto";

        return "Agora mesmo";
    }
}