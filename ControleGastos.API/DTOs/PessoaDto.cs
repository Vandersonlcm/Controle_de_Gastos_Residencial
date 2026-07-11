using System.ComponentModel.DataAnnotations;

namespace ControleGastos.API.DTOs;

/// DTO utilizado para cadastrar uma pessoa.
public class PessoaDto
{
    /// Nome da pessoa.
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;


    /// Idade da pessoa.
    [Required]
    [Range(0,120)]
    public int Idade { get; set; }
}