using System;
using System.Collections;
using System.Collections.Generic;
using mge.Models.Categoria;
using mge.Models.Item;

namespace mge.RequestModels.Item
{
    public class AdicionarRequestModel : IDadosBasicosItem 
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public int Categoria {get; set;}
        public decimal ConsumoWatts {get; set;}
        public int HorasUsoDiario {get; set;}

        public ICollection<string> ValidarEFiltrar()
        {
            var errors = new List<string>();

            return errors;
        }
    }
}