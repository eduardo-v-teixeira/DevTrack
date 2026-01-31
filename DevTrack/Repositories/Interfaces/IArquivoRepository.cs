namespace DevTrack.Repositories.Interfaces;

/// <summary>
/// Interface para manipulação de arquivos no sistema de arquivos.
/// Permite gravar, ler, listar e excluir arquivos.
/// </summary>
public interface IArquivoRepository
{
    /// <summary>
    /// Grava conteúdo em um arquivo no diretório especificado.
    /// </summary>
    Task GravarAsync(string caminhoRelativo, string conteudo);

    /// <summary>
    /// Lê o conteúdo de um arquivo.
    /// </summary>
    Task<string> LerAsync(string caminhoRelativo);

    /// <summary>
    /// Lista os nomes dos arquivos em um diretório.
    /// </summary>
    Task<IEnumerable<string>> ListarAsync(string? subdiretorio = null);

    /// <summary>
    /// Verifica se um arquivo existe.
    /// </summary>
    Task<bool> ExisteAsync(string caminhoRelativo);

    /// <summary>
    /// Exclui um arquivo se existir.
    /// </summary>
    Task ExcluirAsync(string caminhoRelativo);
}
