using System.Collections;
using System.Collections.Generic;
using mge.Models.Categoria;

namespace mge.RequestModels.Categoria
{
    public class AdicionarRequestModel : IDadosBasicosCategoria 
    {
        public string Descricao { get; set; }
        public int CategoriaPai { get; set; }

        public ICollection<string> ValidarEFiltrar()
        {
            var errors = new List<string>();

            return errors;
        }
    }
}