namespace MyFinances.Spendings
{
    public class SpendingModel
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public string Observacao { get; set; }
    }
}
