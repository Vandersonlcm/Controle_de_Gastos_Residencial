namespace ControleGastos.API.DTOs;

/// DTO utilizado para retornar uma transação juntamente com o nome da pessoa.
public class TransacaoResponseDto
{
    /// Identificador da transação.
    public int Id { get; set; }

    /// Descrição da transação.
    public string Descricao { get; set; } = string.Empty;

    /// Valor da transação.
    public decimal Valor { get; set; }

    /// Tipo da transação.
    public string Tipo { get; set; } = string.Empty;

    /// Identificador da pessoa.
    public int PessoaId { get; set; }

    /// Nome da pessoa.
    public string NomePessoa { get; set; } = string.Empty;
}