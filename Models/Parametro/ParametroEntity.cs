namespace mge.Models.Parametro
{
    public class ParametroEntity
    {
        public int Id { get; set; }
        public decimal ValorKwh { get; set; }
        public decimal FaixaConsumoAlto { get; set; }
        public decimal FaixaConsumoMedio { get; set; }
    }
}