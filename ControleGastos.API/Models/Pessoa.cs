using System.ComponentModel.DataAnnotations;

namespace ControleGastos.API.Models;

/// Representa uma pessoa cadastrada no sistema.
/// Cada pessoa poderá possuir várias transações.
public class Pessoa
{
    /// Identificador único da pessoa.
    /// O Entity Framework gera esse valor automaticamente.
    [Key]
    public int Id { get; set; }

    /// Nome da pessoa.
    /// Campo obrigatório.
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    /// Idade da pessoa.
    /// Utilizada para validar se poderá cadastrar receitas.
    [Required]
    [Range(0, 120)]
    public int Idade { get; set; }

    /// Lista de transações pertencentes à pessoa.
    /// Um relacionamento de um para muitos.
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}