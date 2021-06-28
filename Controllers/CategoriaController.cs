using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mge.Models;
using mge.Models.Categoria;
using mge.RequestModels.Categoria;
using mge.ViewModels.Categoria;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mge.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly CategoriaService _categoriaService;

        public CategoriaController(ILogger<CategoriaController> logger, CategoriaService categoriaService)
        {
            _logger = logger;
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel()
            {
                Mensagem = (string) TempData["mensagem"], 
                Error = (string) TempData["error"]
            };
            var lista = _categoriaService.ObterTodos();
            vm.Categorias = lista;
            return View(vm);
        }
        
        [HttpGet]
        public IActionResult Adicionar()
        {
            var vm = new AdicionarViewModel()
            {
                errors = (string[]) TempData["errors"]
            };
            
            var lista = _categoriaService.ObterPais();
            
            foreach (var c in lista)
            {
                vm.CategoriasPai.Add(new SelectListItem(c.Descricao, c.Id.ToString()));
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
                _categoriaService.Adicionar(requestModel);
                TempData["mensagem"] = "Categoria adicionada com sucesso!";
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
                var ce = _categoriaService.ObterPorId(id);

                var vm = new EditarViewModel()
                {
                    Id = ce.Id,
                    Descricao = ce.Descricao,
                    CategoriaPai = ce.CategoriaPaiId,
                    errors = (string[]) TempData["errors"]
                };

                var lista = _categoriaService.ObterPais();
            
                foreach (var c in lista)
                {
                    vm.CategoriasPai.Add(new SelectListItem(c.Descricao, c.Id.ToString()));
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
                _categoriaService.Editar(Id, requestModel);
                TempData["mensagem"] = "Categoria editado com sucesso!";
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
                var ce = _categoriaService.ObterPorId(id);

                var vm = new DeletarViewModel()
                {
                    Id = ce.Id,
                    Descricao = ce.Descricao,
                    errors = (string[]) TempData["errors"]
                };

                if (ce.CategoriaPaiId != 0)
                {
                    var pai = _categoriaService.ObterPorId(ce.CategoriaPaiId);
                    vm.NomeCategoriaPai = pai.Descricao;
                }
                else
                {
                    vm.NomeCategoriaPai = "Nenhum";
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
        public RedirectToActionResult Deletar(int Id, object requestModel)
        {
            try
            {
                _categoriaService.Remover(Id);
                TempData["mensagem"] = "Categoria deletada com sucesso!";
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