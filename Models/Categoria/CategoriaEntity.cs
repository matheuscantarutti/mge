using System;

namespace mge.Models.Categoria
{
    public class CategoriaEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CategoriaPaiId { get; set; }
    }
}