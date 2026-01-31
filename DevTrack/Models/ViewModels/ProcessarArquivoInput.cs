using System.ComponentModel.DataAnnotations;

namespace DevTrack.Models.ViewModels;

/// <summary>
/// Modelo de entrada para a ação de processar/gravar arquivo.
/// </summary>
public class ProcessarArquivoInput
{
    [Required(ErrorMessage = "O nome do arquivo é obrigatório.")]
    [Display(Name = "Nome do arquivo")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 100 caracteres.")]
    public string NomeArquivo { get; set; } = string.Empty;

    [Display(Name = "Conteúdo")]
    [StringLength(5000, ErrorMessage = "O conteúdo não pode ultrapassar 5000 caracteres.")]
    public string Conteudo { get; set; } = string.Empty;
}
