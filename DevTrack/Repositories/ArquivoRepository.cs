using DevTrack.Repositories.Interfaces;

namespace DevTrack.Repositories;

/// <summary>
/// Implementação do repositório de arquivos usando System.IO.
/// Os arquivos são armazenados em wwwroot/arquivos (ou diretório configurável).
/// </summary>
public class ArquivoRepository : IArquivoRepository
{
    private readonly string _diretorioBase;
    private readonly ILogger<ArquivoRepository> _logger;

    public ArquivoRepository(IWebHostEnvironment env, ILogger<ArquivoRepository> logger)
    {
        _diretorioBase = Path.Combine(env.WebRootPath, "arquivos");
        _logger = logger;
        GarantirDiretorioExiste();
    }

    private void GarantirDiretorioExiste()
    {
        if (!Directory.Exists(_diretorioBase))
        {
            Directory.CreateDirectory(_diretorioBase);
            _logger.LogInformation("Diretório de arquivos criado: {Diretorio}", _diretorioBase);
        }
    }

    private string ObterCaminhoCompleto(string caminhoRelativo)
    {
        var caminho = Path.Combine(_diretorioBase, caminhoRelativo);
        var caminhoNormalizado = Path.GetFullPath(caminho);

        // Segurança: garante que o caminho está dentro do diretório base
        if (!caminhoNormalizado.StartsWith(_diretorioBase, StringComparison.OrdinalIgnoreCase))
        {
            throw new UnauthorizedAccessException("Caminho inválido: fora do diretório permitido.");
        }

        return caminhoNormalizado;
    }

    public async Task GravarAsync(string caminhoRelativo, string conteudo)
    {
        var caminhoCompleto = ObterCaminhoCompleto(caminhoRelativo);
        var diretorio = Path.GetDirectoryName(caminhoCompleto)!;

        if (!Directory.Exists(diretorio))
            Directory.CreateDirectory(diretorio);

        await File.WriteAllTextAsync(caminhoCompleto, conteudo);
        _logger.LogInformation("Arquivo gravado: {Arquivo}", caminhoRelativo);
    }

    public async Task<string> LerAsync(string caminhoRelativo)
    {
        var caminhoCompleto = ObterCaminhoCompleto(caminhoRelativo);

        if (!File.Exists(caminhoCompleto))
            throw new FileNotFoundException("Arquivo não encontrado.", caminhoRelativo);

        return await File.ReadAllTextAsync(caminhoCompleto);
    }

    public Task<IEnumerable<string>> ListarAsync(string? subdiretorio = null)
    {
        var diretorio = string.IsNullOrWhiteSpace(subdiretorio)
            ? _diretorioBase
            : ObterCaminhoCompleto(subdiretorio);

        if (!Directory.Exists(diretorio))
            return Task.FromResult(Enumerable.Empty<string>());

        var arquivos = Directory.EnumerateFiles(diretorio)
            .Select(Path.GetFileName)
            .OfType<string>();

        return Task.FromResult(arquivos);
    }

    public Task<bool> ExisteAsync(string caminhoRelativo)
    {
        var caminhoCompleto = ObterCaminhoCompleto(caminhoRelativo);
        return Task.FromResult(File.Exists(caminhoCompleto));
    }

    public Task ExcluirAsync(string caminhoRelativo)
    {
        var caminhoCompleto = ObterCaminhoCompleto(caminhoRelativo);

        if (File.Exists(caminhoCompleto))
        {
            File.Delete(caminhoCompleto);
            _logger.LogInformation("Arquivo excluído: {Arquivo}", caminhoRelativo);
        }

        return Task.CompletedTask;
    }
}
