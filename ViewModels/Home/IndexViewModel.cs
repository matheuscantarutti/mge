using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mge.ViewModels.Home
{
    public class IndexViewModel
    {
        public ICollection<CategoriaConsumista> CategoriasConsumistas;

        public ICollection<ItemConsumista> ItensConsumistas;
        public string ConsumoMensal;
        public string ValorMensal;
        public string faixaConsumo;

        public IndexViewModel()
        {
            CategoriasConsumistas = new List<CategoriaConsumista> ();
            ItensConsumistas = new List<ItemConsumista>();
        }
    }

    public class CategoriaConsumista
    {
        public string  Posicao { get; set; }
        public string  Categoria { get; set; }
        public string ConsumoMensalKwh { get; set; }
        public string ValorMensalKwh { get; set; }

    }

    public class ItemConsumista
    {
        public string Posicao { get; set; }
        public string Item { get; set; }
        public string ConsumoMensalKwh { get; set; }
        public string ValorMensalKwh { get; set; }
    }

    
}
