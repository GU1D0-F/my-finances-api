namespace MyFinances.Spendings
{
    public class SpendingDto
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public string Observacao { get; set; }

        public string DataFormatada { get => Data.ToString("dd/MM/yyyy"); }
    }
}
