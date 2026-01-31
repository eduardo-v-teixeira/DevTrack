using DevTrack.Models.ViewModels;
using DevTrack.Repositories.Interfaces;
using DevTrack.Services.Interfaces;

namespace DevTrack.Services;

/// <summary>
/// Camada de negócio para operações com arquivos.
/// Contém as regras de negócio antes de delegar ao repositório.
/// </summary>
public class ArquivoService : IArquivoService
{
    private readonly IArquivoRepository _arquivoRepository;
    private readonly ILogger<ArquivoService> _logger;

    private static readonly string[] ExtensoesPermitidas = { ".txt", ".md", ".json", ".log" };
    private static readonly char[] CaracteresProibidos = Path.GetInvalidFileNameChars();

    public ArquivoService(IArquivoRepository arquivoRepository, ILogger<ArquivoService> logger)
    {
        _arquivoRepository = arquivoRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<ResultadoProcessamento> ProcessarArquivoAsync(ProcessarArquivoInput input)
    {
        // --- Regras de negócio ---

        var validacao = ValidarNomeArquivo(input.NomeArquivo);
        if (!validacao.EValido)
            return ResultadoProcessamento.Erro(validacao.Mensagem!);

        validacao = ValidarExtensao(input.NomeArquivo);
        if (!validacao.EValido)
            return ResultadoProcessamento.Erro(validacao.Mensagem!);

        validacao = ValidarConteudo(input.Conteudo);
        if (!validacao.EValido)
            return ResultadoProcessamento.Erro(validacao.Mensagem!);

        // --- Execução via repositório ---

        try
        {
            var caminhoRelativo = SanitizarNomeArquivo(input.NomeArquivo);
            await _arquivoRepository.GravarAsync(caminhoRelativo, input.Conteudo);
            _logger.LogInformation("Arquivo processado pela regra de negócio: {Arquivo}", caminhoRelativo);
            return ResultadoProcessamento.Ok(caminhoRelativo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar arquivo {Arquivo}", input.NomeArquivo);
            return ResultadoProcessamento.Erro($"Erro ao gravar arquivo: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public Task<IEnumerable<string>> ListarArquivosAsync() =>
        _arquivoRepository.ListarAsync();

    /// <inheritdoc />
    public async Task<string?> ObterConteudoAsync(string nomeArquivo)
    {
        if (string.IsNullOrWhiteSpace(nomeArquivo))
            return null;

        var existe = await _arquivoRepository.ExisteAsync(nomeArquivo);
        if (!existe)
            return null;

        return await _arquivoRepository.LerAsync(nomeArquivo);
    }

    #region Regras de negócio

    private static (bool EValido, string? Mensagem) ValidarNomeArquivo(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return (false, "O nome do arquivo não pode ser vazio.");

        if (nome.Length > 100)
            return (false, "O nome do arquivo não pode ultrapassar 100 caracteres.");

        if (nome.IndexOfAny(CaracteresProibidos) >= 0)
            return (false, "O nome contém caracteres inválidos.");

        // Evitar path traversal
        if (nome.Contains("..") || nome.Contains("/") || nome.Contains("\\"))
            return (false, "O nome do arquivo não pode conter caminhos relativos.");

        return (true, null);
    }

    private static (bool EValido, string? Mensagem) ValidarExtensao(string nome)
    {
        var ext = Path.GetExtension(nome).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext))
            return (false, $"Informe uma extensão válida. Permitidas: {string.Join(", ", ExtensoesPermitidas)}");

        if (!ExtensoesPermitidas.Contains(ext))
            return (false, $"Extensão '{ext}' não permitida. Use: {string.Join(", ", ExtensoesPermitidas)}");

        return (true, null);
    }

    private static (bool EValido, string? Mensagem) ValidarConteudo(string conteudo)
    {
        if (conteudo.Length > 5000)
            return (false, "O conteúdo não pode ultrapassar 5000 caracteres.");

        return (true, null);
    }

    private static string SanitizarNomeArquivo(string nome)
    {
        return nome.Trim();
    }

    #endregion
}
