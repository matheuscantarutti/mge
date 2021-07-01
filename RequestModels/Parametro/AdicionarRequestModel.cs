using System.Collections;
using System.Collections.Generic;
using mge.Models.Parametro;

namespace mge.RequestModels.Parametro
{
    public class AdicionarRequestModel : IDadosBasicosParametro
    {
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }

        public ICollection<string> ValidarEFiltrar()
        {
            var errors = new List<string>();

            return errors;
        }   
    }
}