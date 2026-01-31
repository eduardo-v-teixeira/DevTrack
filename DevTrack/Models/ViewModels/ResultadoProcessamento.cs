namespace DevTrack.Models.ViewModels;

/// <summary>
/// Resultado da operação de processamento de arquivo.
/// </summary>
public class ResultadoProcessamento
{
    public bool Sucesso { get; init; }
    public string Mensagem { get; init; } = string.Empty;
    public string? NomeArquivo { get; init; }

    public static ResultadoProcessamento Ok(string nomeArquivo) =>
        new()
        {
            Sucesso = true,
            Mensagem = "Arquivo processado e gravado com sucesso.",
            NomeArquivo = nomeArquivo
        };

    public static ResultadoProcessamento Erro(string mensagem) =>
        new()
        {
            Sucesso = false,
            Mensagem = mensagem
        };
}
