using mge.Data;
using mge.Models.Categoria;
using mge.Models.Item;
using mge.Models.Parametro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mge.Models
{
    public class RelatorioService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ParametroService _parametroService;
        private readonly ItemService _itemService;
        private readonly CategoriaService _categoriaService;

        public RelatorioService(DatabaseContext databaseContext, ParametroService parametroService, ItemService itemService, CategoriaService categoriaService)
        {
            _databaseContext = databaseContext;
            _parametroService = parametroService;
            _itemService = itemService;
            _categoriaService = categoriaService;
        }

        public ICollection<ConsumoCategoria> categoriasConsumistas(){

            var parametro = _parametroService.ObterTodos().First();
            var categorias = _categoriaService.ObterTodos();
            var consumos = new List<ConsumoCategoria>();

            foreach (var categoriaEntity in categorias) {
                var itemDestacategoria = _itemService.ObterItensPorCategoria(categoriaEntity.Id);
                decimal consumoMensalItens = 0;

                foreach (var item in itemDestacategoria) {
                    consumoMensalItens += item.CalcGastoEnergeticoMensal();
                }

                consumos.Add(new ConsumoCategoria()
                {
                    Categoria = categoriaEntity.Descricao,
                    ConsumoMensalKwh = consumoMensalItens,
                    ValorMensalKwh = parametro.ValorKwh * consumoMensalItens
                });
            }

            return consumos.OrderByDescending(c => c.ConsumoMensalKwh).Take(3).ToList();
        }
    }

    public class ConsumoCategoria
    {
        public string Categoria { get; set; }

        public decimal ConsumoMensalKwh { get; set; }

        public decimal ValorMensalKwh { get; set; }

    }
}
