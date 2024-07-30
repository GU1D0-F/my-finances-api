using MyFinances.Users;

namespace MyFinances.Spendings
{
    public class Spending
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacaoEnum TipoTransacao { get; set; }
        public string Observacao { get; set; }
        public string UserId { get; set; }


        public User User { get; set; }
    }
}
