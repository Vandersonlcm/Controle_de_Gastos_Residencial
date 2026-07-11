using ControleGastos.API.Data;
using ControleGastos.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Controllers;

/// Controller responsável pela consulta de totais.
[ApiController]
[Route("api/[controller]")]
public class TotaisController : ControllerBase
{
    private readonly AppDbContext _context;

    public TotaisController(AppDbContext context)
    {
        _context = context;
    }

    /// Retorna os totais por pessoa e o total geral.
    [HttpGet]
    public async Task<ActionResult> Consultar()
    {
        var pessoas = await _context.Pessoas
            .Include(p => p.Transacoes)
            .ToListAsync();

        var resultado = pessoas.Select(p => new TotaisDto
        {
            PessoaId = p.Id,
            Nome = p.Nome,

            TotalReceitas = p.Transacoes
                .Where(t => t.Tipo == "Receita")
                .Sum(t => t.Valor),

            TotalDespesas = p.Transacoes
                .Where(t => t.Tipo == "Despesa")
                .Sum(t => t.Valor)
        }).ToList();

        var totalReceitas = resultado.Sum(x => x.TotalReceitas);
        var totalDespesas = resultado.Sum(x => x.TotalDespesas);

        return Ok(new
        {
            pessoas = resultado,

            totalReceitas,

            totalDespesas,

            saldoLiquido = totalReceitas - totalDespesas
        });
    }
}