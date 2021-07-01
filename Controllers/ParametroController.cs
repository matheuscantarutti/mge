using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mge.Models;
using mge.ViewModels.Parametro;
using mge.Models.Parametro;
using mge.RequestModels.Parametro;

namespace mge.Controllers
{
    public class ParametroController : Controller
    {
        private readonly ILogger<ParametroController> _logger;
        private readonly ParametroService _parametroService;

        public ParametroController(ILogger<ParametroController> logger, ParametroService parametroService)
        {
            _logger = logger;
            _parametroService = parametroService;

        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel()
            {
                Mensagem = (string)TempData["mensagem"],
                Error = (string)TempData["error"]
            };


            var lista = _parametroService.ObterTodos();

            foreach (ParametroEntity parametro in lista) {
                vm.Parametros.Add(new Parametros()
                {
                    Id = parametro.Id.ToString(),
                    Valorkwh = parametro.ValorKwh.ToString("C"),
                    FaixaConsumoAlta = parametro.FaixaConsumoMedio.ToString("N"),
                    FaixaConsumoMedia = parametro.FaixaConsumoMedio.ToString("N")
                }); 
            }


            return View(vm);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            var vm = new AdicionarViewModel()
            {
                errors = (string[])TempData["errors"]
            };

            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Adicionar(AdicionarRequestModel requestModel)
        {
            var errors = requestModel.ValidarEFiltrar();
            if (errors.Count > 0)
            {
                TempData["errors"] = errors;

                return RedirectToAction("Adicionar");
            }

            try
            {
                _parametroService.Adicionar(requestModel);
                TempData["mensagem"] = "Parâmetro adicionado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["errors"] = new List<string> { e.Message };
                return RedirectToAction("Adicionar");
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            try
            {
                var ce = _parametroService.ObterPorId(id);

                var vm = new EditarViewModel()
                {
                    Id = ce.Id.ToString(),
                    Valorkwh = ce.ValorKwh.ToString("N"),
                    FaixaConsumoAlto = ce.FaixaConsumoMedio.ToString("N"),
                    FaixaConsumoMedio = ce.FaixaConsumoMedio.ToString("N"),
                    errors = (string[])TempData["errors"]
                };

                return View(vm);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Editar(int Id, EditarRequestModel requestModel)
        {
            var errors = requestModel.ValidarEFiltrar();
            if (errors.Count > 0)
            {
                TempData["errors"] = errors;

                return RedirectToAction("Adicionar");
            }

            try
            {
                _parametroService.Editar(Id, requestModel);
                TempData["mensagem"] = "Categoria editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["errors"] = new List<string> { e.Message };
                return RedirectToAction("Editar");
            }
        }


        [HttpGet]
        public IActionResult Deletar(int id)
        {
            try
            {
                var ce = _parametroService.ObterPorId(id);

                var vm = new DeletarViewModel()
                {
                    Id = ce.Id.ToString(),
                    ValorKwh = ce.ValorKwh.ToString("C"),
                    FaixaConsumoAlto = ce.FaixaConsumoAlto.ToString("N"),
                    FaixaConsumoMedio = ce.FaixaConsumoMedio.ToString("N"),
                    errors = (string[])TempData["errors"]
                };

                return View(vm);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Deletar(int Id, object requestModel)
        {
            try
            {
                _parametroService.Remover(Id);
                TempData["mensagem"] = "Parâmetro deletado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}