using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mge.Models;
using mge.ViewModels.Home;

namespace mge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RelatorioService _relatorioService;


        public HomeController(ILogger<HomeController> logger, RelatorioService relatorioService)
        {
            _logger = logger;
            _relatorioService = relatorioService;
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            var categoiasConsumistas = _relatorioService.categoriasConsumistas();

            var posicao = 1;

            foreach (var consumoCategoria in categoiasConsumistas) {
                vm.CategoriasConsumistas.Add(new CategoriaConsumista()
                {
                    Posicao = (posicao++).ToString(),
                    Categoria = consumoCategoria.Categoria,
                    ConsumoMensalKwh = consumoCategoria.ConsumoMensalKwh.ToString("N"),
                    ValorMensalKwh = consumoCategoria.ValorMensalKwh.ToString("C")
                });
            }

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}