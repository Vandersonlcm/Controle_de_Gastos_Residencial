namespace ControleGastos.API.DTOs;

/// Representa os totais financeiros de uma pessoa.
public class TotaisDto
{
    /// Identificador da pessoa.
    public int PessoaId { get; set; }

    /// Nome da pessoa.
    public string Nome { get; set; } = string.Empty;

    /// Soma das receitas.
    public decimal TotalReceitas { get; set; }

    /// Soma das despesas.
    public decimal TotalDespesas { get; set; }

    /// Saldo da pessoa.
    public decimal Saldo => TotalReceitas - TotalDespesas;
}