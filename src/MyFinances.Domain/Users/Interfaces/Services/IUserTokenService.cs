using Microsoft.AspNetCore.Identity;

namespace MyFinances.Users
{
    public interface IUserTokenService
    {
        string GenerateToken(User user);
    }
}
