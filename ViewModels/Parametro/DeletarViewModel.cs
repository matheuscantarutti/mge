namespace mge.ViewModels.Parametro
{
    public class DeletarViewModel
    {
        public string[] errors { get; set; }
        public string Id { get; set; }
        public string ValorKwh { get; internal set; }
        public string FaixaConsumoAlto { get; internal set; }
        public string FaixaConsumoMedio { get; internal set; }
    }
}