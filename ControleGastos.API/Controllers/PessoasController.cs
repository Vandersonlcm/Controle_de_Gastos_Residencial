using ControleGastos.API.Data;
using ControleGastos.API.DTOs;
using ControleGastos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Controllers;

/// Controller responsável pelo gerenciamento das pessoas.
[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly AppDbContext _context;

    /// Recebe o contexto do banco através da Injeção de Dependência.
    public PessoasController(AppDbContext context)
    {
        _context = context;
    }

    /// Retorna todas as pessoas cadastradas.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pessoa>>> Listar()
    {
        var pessoas = await _context.Pessoas
            .AsNoTracking()
            .ToListAsync();

        return Ok(pessoas);
    }

    /// Cadastra uma nova pessoa.
    [HttpPost]
    public async Task<ActionResult> Cadastrar(PessoaDto dto)
    {
        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Idade = dto.Idade
        };

        _context.Pessoas.Add(pessoa);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Listar), new { id = pessoa.Id }, pessoa);
    }

    /// Remove uma pessoa.
    /// As transações serão removidas automaticamente devido ao Delete Cascade.
    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa == null)
            return NotFound("Pessoa não encontrada.");

        _context.Pessoas.Remove(pessoa);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}