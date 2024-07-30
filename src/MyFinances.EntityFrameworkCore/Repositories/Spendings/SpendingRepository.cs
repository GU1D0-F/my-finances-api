using Microsoft.EntityFrameworkCore;
using MyFinances.Spendings;

namespace MyFinances.EntityFrameworkCore.Repositories.Spendings
{
    public class SpendingRepository : ISpendingRepository
    {
        private readonly MyFinancesDbContext _dbContext;
        protected readonly DbSet<Spending> _dbSet;

        public SpendingRepository(MyFinancesDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.SpendingDbSet;
        }

        public IQueryable<Spending> GetItems(string userId) =>
           _dbSet.Where(itens => itens.UserId == userId);

        public async Task<Spending> GetItemById(long id, string userId) =>
            await _dbSet.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);

        public async Task<SpendingDto> AddItem(Spending item)
        {
            await _dbSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return new SpendingDto()
            {
                Id = item.Id,
                Data = item.Data,
                Descricao = item.Descricao,
                Observacao = item.Observacao,
                TipoTransacao = item.TipoTransacao,
                Valor = item.Valor,
            };
        }

        public async Task RemoveItem(Spending item)
        {
            _dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
        }


        public async Task RemoveItens(List<long> ids, string userId)
        {
            var entitiesToRemove = _dbSet.Where(x => ids.Contains(x.Id) && x.UserId == userId).ToList();

            _dbSet.RemoveRange(entitiesToRemove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SpendingDto> UpdateItem(long id, SpendingModel item, string userId)
        {
            var reg = await GetItemById(id, userId);

            if (reg is null)
                return null;

            reg.Data = item.Data;
            reg.Descricao = item.Descricao;
            reg.Observacao = item.Observacao;
            reg.TipoTransacao = item.TipoTransacao;
            reg.Valor = item.Valor;


            _dbSet.Update(reg);
            await _dbContext.SaveChangesAsync();

            return new SpendingDto()
            {
                Id = reg.Id,
                Data = reg.Data,
                Descricao = reg.Descricao,
                Observacao = reg.Observacao,
                TipoTransacao = reg.TipoTransacao,
                Valor = reg.Valor
            };
        }
    }
}
