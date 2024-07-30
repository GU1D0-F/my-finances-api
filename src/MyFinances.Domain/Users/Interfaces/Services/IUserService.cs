using Microsoft.AspNetCore.Identity;

namespace MyFinances.Users
{
    public interface IUserService
    {
        Task<UserLoginResponseModel> LoginAsync(string email, string passaword);
        Task<IdentityResult> RegisterAsync(UserRegisterModel userModel);
    }
}
