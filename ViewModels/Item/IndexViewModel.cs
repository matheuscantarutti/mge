using System.Collections;
using System.Collections.Generic;
using mge.Models.Item;

namespace mge.ViewModels.Item
{
    public class IndexViewModel
    {
        public List<Item> Items { get; set; }
        public string Mensagem { get; set; }
        public string Error { get; set; }

        public IndexViewModel(){
            Items = new List<Item>();
        }
    }

    public class Item {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public string Categoria {get; set;}
        public string ConsumoWatts {get; set;}
        public int HorasUsoDiario {get; set;}
    }
}