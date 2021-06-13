using System.Collections;
using System.Collections.Generic;
using mge.Models.Categoria;

namespace mge.ViewModels.Categoria
{
    public class IndexViewModel
    {
        public ICollection<CategoriaEntity> Categorias { get; set; }
        public string Mensagem { get; set; }
        public string Error { get; set; }
    }
}