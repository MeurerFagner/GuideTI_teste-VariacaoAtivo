namespace VariacaoAtivo.Dominio.Entidade
{
    public class Pregao
    {
        public int Id { get; set; }
        public int IdAtivo { get; set; }
        public DateTime DataPregao { get; set; }
        public float? Open { get; set; }
        public float? Low { get; set; }
        public float? High { get; set; }
        public float? Close { get; set; }
        public int? Volume { get; set; }
        public DateTime DataCadastro { get; set;}
        public Ativo Ativo { get; set; }
    }
}
