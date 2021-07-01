using System.Collections;
using System.Collections.Generic;
using mge.Models.Parametro;

namespace mge.ViewModels.Parametro
{
    public class IndexViewModel
    {
        public ICollection<Parametros> Parametros{ get; set; }
        public string Mensagem { get; set; }
        public string Error { get; set; }

        public IndexViewModel() => Parametros = new List<Parametros>();
    }

    public class Parametros
    {
        public string Id { get; set; }
        public string Valorkwh { get; set; }
        public string FaixaConsumoAlta { get; set; }
        public string FaixaConsumoMedia { get; set; }
    }
}