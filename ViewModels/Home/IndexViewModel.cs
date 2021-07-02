using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mge.ViewModels.Home
{
    public class IndexViewModel
    {
        public ICollection<CategoriaConsumista> CategoriasConsumistas;

        public IndexViewModel()
        {
            CategoriasConsumistas = new List<CategoriaConsumista> ();
        }
    }

    public class CategoriaConsumista
    {
        public string  Posicao { get; set; }
        public string  Categoria { get; set; }
        public string ConsumoMensalKwh { get; set; }
        public string ValorMensalKwh { get; set; }

    }

    
}
