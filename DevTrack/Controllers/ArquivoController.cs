using DevTrack.Models.ViewModels;
using DevTrack.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevTrack.Controllers;

/// <summary>
/// Controller para ações relacionadas a arquivos.
/// Delega a lógica de negócio para o ArquivoService.
/// </summary>
public class ArquivoController : Controller
{
    private readonly IArquivoService _arquivoService;

    public ArquivoController(IArquivoService arquivoService)
    {
        _arquivoService = arquivoService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var arquivos = await _arquivoService.ListarArquivosAsync();
        ViewBag.Arquivos = arquivos;
        return View(new ProcessarArquivoInput());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Processar(ProcessarArquivoInput input)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Arquivos = await _arquivoService.ListarArquivosAsync();
            return View("Index", input);
        }

        var resultado = await _arquivoService.ProcessarArquivoAsync(input);

        if (resultado.Sucesso)
        {
            TempData["MensagemSucesso"] = resultado.Mensagem;
            TempData["ArquivoCriado"] = resultado.NomeArquivo;
        }
        else
        {
            TempData["MensagemErro"] = resultado.Mensagem;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Visualizar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return BadRequest();

        var conteudo = await _arquivoService.ObterConteudoAsync(nome);
        if (conteudo == null)
            return NotFound();

        ViewBag.NomeArquivo = nome;
        ViewBag.Conteudo = conteudo;
        return View();
    }
}
