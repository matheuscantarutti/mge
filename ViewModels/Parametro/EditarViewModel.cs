using System.Collections.Generic;
using mge.Models.Parametro;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mge.ViewModels.Parametro
{
    public class EditarViewModel
    {
        public string[] errors { get; set; }
        public string Id { get; internal set; }
        public string Valorkwh { get; internal set; }
        public string FaixaConsumoMedio { get; internal set; }
        public string FaixaConsumoAlto { get; internal set; }
    }
}