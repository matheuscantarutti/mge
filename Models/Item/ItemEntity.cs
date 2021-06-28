using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mge.Models.Categoria;

namespace mge.Models.Item
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public CategoriaEntity Categoria {get; set;}
        
        [DataType(DataType.Currency)]
        [Column(TypeName = ("decimal(15,2)"))]
        public decimal ConsumoWatts {get; set;}
        public int HorasUsoDiario {get; set;}
    }
}