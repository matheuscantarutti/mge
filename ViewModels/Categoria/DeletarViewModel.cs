namespace mge.ViewModels.Categoria
{
    public class DeletarViewModel
    {
        public string[] errors { get; set; }
        public int Id { get; set; }
        
        public string Descricao { get; set; }
        
        public string NomeCategoriaPai { get; set; }
    }
}