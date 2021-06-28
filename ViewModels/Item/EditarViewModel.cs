using System;
using System.Collections.Generic;
using mge.Models.Categoria;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mge.ViewModels.Item
{
    public class EditarViewModel
    {
        public string[] errors { get; set; }
        public ICollection<SelectListItem> Categorias { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public int Categoria {get; set;}
        public decimal ConsumoWatts {get; set;}
        public int HorasUsoDiario {get; set;}

        public EditarViewModel()
        {
            Categorias = new List<SelectListItem>();
        }
    }
}