using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGastos.API.Models;

/// Representa uma movimentação financeira.
/// Pode ser Receita ou Despesa.
public class Transacao
{
    /// Identificador único da transação.
    [Key]
    public int Id { get; set; }

    /// Descrição da movimentação.
    [Required]
    [MaxLength(200)]
    public string Descricao { get; set; } = string.Empty;

    /// Valor da transação.
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Valor { get; set; }

    /// Tipo da transação.
    /// Os valores aceitos serão:
    /// Receita
    /// Despesa
    [Required]
    [MaxLength(20)]
    public string Tipo { get; set; } = string.Empty;

    /// Chave estrangeira para Pessoa.
    [Required]
    public int PessoaId { get; set; }

    /// Navegação para a pessoa proprietária da transação.
    public Pessoa? Pessoa { get; set; }
}