using Microsoft.AspNetCore.Identity;
using MyFinances.Spendings;

namespace MyFinances.Users
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }


        public List<Spending> Spendings { get; set; }
    }
}
