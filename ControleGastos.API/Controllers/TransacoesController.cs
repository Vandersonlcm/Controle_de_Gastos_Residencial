using ControleGastos.API.Data;
using ControleGastos.API.DTOs;
using ControleGastos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Controllers;

/// Controller responsável pelo gerenciamento das transações.
[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransacoesController(AppDbContext context)
    {
        _context = context;
    }

    /// Lista todas as transações cadastradas.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoResponseDto>>> Listar()
    {
        var transacoes = await _context.Transacoes
            .Include(t => t.Pessoa)
            .Select(t => new TransacaoResponseDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo,
                PessoaId = t.PessoaId,
                NomePessoa = t.Pessoa != null ? t.Pessoa.Nome : ""
            })
            .AsNoTracking()
            .ToListAsync();

        return Ok(transacoes);
    }

    /// Cadastra uma nova transação.
    [HttpPost]
    public async Task<ActionResult> Cadastrar(TransacaoDto dto)
    {
        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);

        if (pessoa == null)
            return NotFound("Pessoa não encontrada.");

        if (pessoa.Idade < 18 &&
            dto.Tipo.Equals("Receita", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Pessoas menores de 18 anos podem cadastrar apenas despesas.");
        }

        if (!dto.Tipo.Equals("Receita", StringComparison.OrdinalIgnoreCase) &&
            !dto.Tipo.Equals("Despesa", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("O tipo deve ser Receita ou Despesa.");
        }

        var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Tipo = dto.Tipo,
            PessoaId = dto.PessoaId
        };

        _context.Transacoes.Add(transacao);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensagem = "Transação cadastrada com sucesso.",
            id = transacao.Id
        });
    }
}