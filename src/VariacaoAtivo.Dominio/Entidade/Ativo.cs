namespace VariacaoAtivo.Dominio.Entidade
{
    public class Ativo
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Currency { get; set; }
        public virtual ICollection<Pregao> Pregoes { get; set; }
        public DateTime FirstDateTrade { get; set; }
        public DateTime DataCadastro { get; set;}

    }
}
