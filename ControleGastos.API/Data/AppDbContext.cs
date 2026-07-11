using ControleGastos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Data;

/// Contexto responsável pela comunicação entre a aplicação
/// e o banco de dados MySQL.
public class AppDbContext : DbContext
{
    /// Construtor utilizado pelo Entity Framework.
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// Tabela Pessoas.
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();

    /// Tabela Transacoes.
    public DbSet<Transacao> Transacoes => Set<Transacao>();

    /// Configurações dos relacionamentos.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(t => t.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}