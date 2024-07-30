using Microsoft.AspNetCore.Identity;

namespace MyFinances.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserTokenService _userTokenService;

        public UserService
           (
               UserManager<User> userManager,
               SignInManager<User> signInManager,
               IUserTokenService userTokenService
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userTokenService = userTokenService;
        }


        public async Task<UserLoginResponseModel> LoginAsync(string email, string passaword)
        {
            User user = await _userManager.FindByEmailAsync(email) ?? throw new Exception("User not found!");

            var result = await _signInManager.PasswordSignInAsync(user, passaword, false, false);

            if (!result.Succeeded)
                throw new Exception("User or password is incorrect");

            string token = _userTokenService.GenerateToken(user);

            return new()
            {
                UserId = user.Id,
                UserToken = $"Bearer {token}"
            };
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterModel userModel)
        {
            User user = new()
            {
                FullName = $"{userModel.FirstName} {userModel.LastName}",
                UserName = $"{userModel.FirstName} {userModel.LastName[0]}",
                BirthDate = userModel.BirthDate,
                Email = userModel.Email,
                EmailConfirmed = false,
                PhoneNumber = userModel.PhoneNumber,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                RegisterDate = DateTime.Now
            };

            return await _userManager.CreateAsync(user, userModel.Password);
        }
    }
}
