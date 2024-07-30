namespace MyFinances.Spendings
{
    public interface ISpendingService
    {
        List<SpendingDto> GetAll(SpendingFilterModel model, string userId);
        Task<SpendingDto> Add(SpendingModel model, string userId);
        Task<SpendingDto> Edit(long id, SpendingModel model, string userId);
        Task Delete(List<long> ids, string userId);
    }
}
