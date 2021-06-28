using System.Collections.Generic;
using mge.Models.Categoria;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mge.ViewModels.Item
{
    public class AdicionarViewModel
    {
        public string[] errors { get; set; }
        public ICollection<SelectListItem> Categorias { get; set; }

        public AdicionarViewModel()
        {
            Categorias = new List<SelectListItem>();
        }
    }
}