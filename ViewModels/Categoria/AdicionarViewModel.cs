using System.Collections.Generic;
using mge.Models.Categoria;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mge.ViewModels.Categoria
{
    public class AdicionarViewModel
    {
        public string[] errors { get; set; }
        public ICollection<SelectListItem> CategoriasPai { get; set; }

        public AdicionarViewModel()
        {
            CategoriasPai = new List<SelectListItem>
            {
                new SelectListItem("Nenhum", "0")
            };
        }
    }
}