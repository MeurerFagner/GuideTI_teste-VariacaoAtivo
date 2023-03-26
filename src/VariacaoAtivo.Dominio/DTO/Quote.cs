namespace VariacaoAtivo.Dominio.DTO
{
    public class Quote
    {
        public float?[] open { get; set; }
        public float?[] low { get; set; }
        public float?[] high { get; set; }
        public float?[] close { get; set; }
        public long?[] volume { get; set; }
    }

}
