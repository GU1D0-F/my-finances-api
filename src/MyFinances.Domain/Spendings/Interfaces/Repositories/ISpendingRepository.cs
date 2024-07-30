namespace MyFinances.Spendings
{
    public interface ISpendingRepository
    {
        Task<Spending> GetItemById(long id, string userId);
        IQueryable<Spending> GetItems(string userId);
        Task<SpendingDto> AddItem(Spending item);
        Task<SpendingDto> UpdateItem(long id, SpendingModel item, string userId);
        Task RemoveItem(Spending item);
        Task RemoveItens(List<long> ids, string userId);
    }
}
