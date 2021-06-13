using System.Collections.Generic;
using mge.Models.Categoria;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mge.ViewModels.Categoria
{
    public class EditarViewModel
    {
        public string[] errors { get; set; }
        public ICollection<SelectListItem> CategoriasPai { get; set; }
        
        public int Id { get; set; }
        
        public string Descricao { get; set; }
        
        public int CategoriaPai { get; set; }

        public EditarViewModel()
        {
            CategoriasPai = new List<SelectListItem>
            {
                new SelectListItem("Nenhum", "0")
            };
        }
    }
}