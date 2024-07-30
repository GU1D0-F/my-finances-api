using MyFinances.Spendings;
using MyFinances.Utils;

namespace MyFinances.Domain.Spendings.Services
{
    public class SpendingService : ISpendingService
    {
        private readonly ISpendingRepository _spendingRepository;

        public SpendingService(ISpendingRepository spendingRepository)
        {
            _spendingRepository = spendingRepository;
        }

        public List<SpendingDto> GetAll(SpendingFilterModel model, string userId)
        {
            var data = DateTime.Parse(model.Data);

            var month = data.Month;
            var year = data.Year;

            return _spendingRepository.GetItems(userId)
                              .Where(itens => itens.Data.Month == month && itens.Data.Year == year)
                              .WhereIf(!string.IsNullOrEmpty(model.Descricao), itens => itens.Descricao.Contains(model.Descricao))
                              .WhereIf(model.Valor != decimal.Zero, itens => itens.Valor == model.Valor)
                              .WhereIf(model.TipoTransacao != 0, itens => itens.TipoTransacao == (TipoTransacaoEnum)model.TipoTransacao)
                              .Select(item => new SpendingDto
                              {
                                  Id = item.Id,
                                  Data = item.Data,
                                  Categoria = item.Categoria,
                                  Descricao = item.Descricao,
                                  Observacao = item.Observacao,
                                  TipoTransacao = item.TipoTransacao,
                                  Valor = item.Valor
                              })
                              .ToList();
        }


        public async Task<SpendingDto> Add(SpendingModel model, string userId) =>
             await _spendingRepository.AddItem(new()
             {
                 Data = model.Data,
                 Descricao = model.Descricao,
                 Observacao = model.Observacao,
                 TipoTransacao = model.TipoTransacao,
                 Valor = model.Valor,
                 UserId = userId
             });

        public async Task<SpendingDto> Edit(long id, SpendingModel model, string userId) =>
            await _spendingRepository.UpdateItem(id, model, userId);

        public async Task Delete(List<long> ids, string userId) =>
            await _spendingRepository.RemoveItens(ids, userId);

    }
}
