using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mge.Models;
using mge.ViewModels.Item;
using mge.Models.Item;
using mge.Models.Categoria;
using Microsoft.AspNetCore.Mvc.Rendering;
using mge.RequestModels.Item;

namespace mge.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ItemService _itemService;
        private readonly CategoriaService _categoriaService;

        public ItemController(ILogger<ItemController> logger, ItemService itemService, CategoriaService categoriaService)
        {
            _logger = logger;
            _itemService = itemService;
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel()
            {
                Mensagem = (string) TempData["mensagem"], 
                Error = (string) TempData["error"]
            };

            var lista = _itemService.ObterTodos();

            foreach (var item in lista)
            {
                vm.Items.Add(new Item(){
                    Id = item.Id.ToString(),
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    DataFabricacao = item.DataFabricacao.ToShortDateString(),
                    Categoria = item.Categoria.Descricao,
                    ConsumoWatts = item.ConsumoWatts.ToString("N"),
                    HorasUsoDiario = item.HorasUsoDiario
                });
            }
            
            return View(vm);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            var vm = new AdicionarViewModel()
            {
                errors = (string[]) TempData["errors"]
            };
            
            var lista = _categoriaService.ObterTodos();
            
            foreach (var c in lista)
            {
                vm.Categorias.Add(new SelectListItem(c.Descricao, c.Id.ToString()));
            }
            
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
                _itemService.Adicionar(requestModel);
                TempData["mensagem"] = "item adicionado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["errors"] = new List<string> {e.Message};
                return RedirectToAction("Adicionar");
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            try
            {
                var ie = _itemService.ObterPorId(id);

                var vm = new EditarViewModel()
                {
                    Id = ie.Id,
                    Descricao = ie.Descricao,
                    Nome = ie.Nome,
                    DataFabricacao = ie.DataFabricacao,
                    // Categoria = ie.Categoria.Id,
                    ConsumoWatts = ie.ConsumoWatts,
                    HorasUsoDiario = ie.HorasUsoDiario,
                    errors = (string[]) TempData["errors"]
                };

                var lista = _categoriaService.ObterTodos();
            
                foreach (var c in lista)
                {
                    vm.Categorias.Add(new SelectListItem(c.Descricao, c.Id.ToString()));
                }

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
                _itemService.Editar(Id, requestModel);
                TempData["mensagem"] = "Item editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["errors"] = new List<string> {e.Message};
                return RedirectToAction("Editar");
            }
        }


        [HttpGet]
        public IActionResult Deletar(int id)
        {
            try
            {
                var ie = _itemService.ObterPorId(id);

                var vm = new DeletarViewModel()
                {
                    Id = ie.Id,
                    Nome = ie.Nome,
                    errors = (string[]) TempData["errors"]
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
                _itemService.Remover(Id);
                TempData["mensagem"] = "Item deletado com sucesso!";
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