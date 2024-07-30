namespace MyFinances.Users
{
    public record UserRegisterModel(string FirstName, 
                                    string LastName, 
                                    DateTime BirthDate, 
                                    string PhoneNumber, 
                                    string Email, 
                                    string Password);
}
