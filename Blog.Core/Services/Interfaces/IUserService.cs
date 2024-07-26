using Blog.Core.DTOs.Account;
using Blog.DataLayer.Entities.Account;

namespace Blog.Core.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsers();
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        bool IsUserExistByEmail(string email);
        Task<LoginUserResult> LoginUser(LoginUserDTO login/*, bool checkAdminRole = false*/);
        Task<User> GetUserByEmail(string email);

    }
}
