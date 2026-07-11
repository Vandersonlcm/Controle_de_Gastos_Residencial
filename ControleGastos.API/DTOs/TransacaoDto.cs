using System.ComponentModel.DataAnnotations;

namespace ControleGastos.API.DTOs;

/// DTO utilizado para cadastrar uma transação.
public class TransacaoDto
{
    /// Descrição da transação.
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MaxLength(200)]
    public string Descricao { get; set; } = string.Empty;

    /// Valor da transação.
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Valor { get; set; }

    /// Tipo da transação.
    /// Deve ser "Receita" ou "Despesa".
    [Required]
    public string Tipo { get; set; } = string.Empty;

    /// Id da pessoa.
    [Required]
    public int PessoaId { get; set; }
}