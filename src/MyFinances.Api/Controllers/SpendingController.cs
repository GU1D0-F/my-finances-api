using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Spendings;
using MyFinances.Users;

namespace MyFinances.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SpendingController : ControllerBase
    {
        private readonly ISpendingService _spendingService;

        public SpendingController(ISpendingService spendingService)
        {
            _spendingService = spendingService;
        }

        [HttpGet, Authorize]
        public IActionResult Get([FromQuery] SpendingFilterModel model) =>
            Ok(_spendingService.GetAll(model, GetUserIdFromClaim()));

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(SpendingModel model) =>
            Ok(await _spendingService.Add(model, GetUserIdFromClaim()));

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> Update(long id, SpendingModel model) =>
            Ok(await _spendingService.Edit(id, model, GetUserIdFromClaim()));

        [HttpDelete, Authorize]
        public async Task<IActionResult> Delete([FromBody] List<long> ids)
        {
            await _spendingService.Delete(ids, GetUserIdFromClaim());

            return Ok();
        }

        private string GetUserIdFromClaim() =>
           User.Claims.FirstOrDefault(x => x.Type == "id").Value;
    }
}
