using DevTrack.Models.ViewModels;

namespace DevTrack.Services.Interfaces;

/// <summary>
/// Interface do serviço de negócio para operações com arquivos.
/// Centraliza as regras de negócio antes de delegar ao repositório.
/// </summary>
public interface IArquivoService
{
    /// <summary>
    /// Processa e grava um arquivo aplicando as regras de negócio.
    /// </summary>
    Task<ResultadoProcessamento> ProcessarArquivoAsync(ProcessarArquivoInput input);

    /// <summary>
    /// Lista os arquivos disponíveis.
    /// </summary>
    Task<IEnumerable<string>> ListarArquivosAsync();

    /// <summary>
    /// Obtém o conteúdo de um arquivo.
    /// </summary>
    Task<string?> ObterConteudoAsync(string nomeArquivo);
}
