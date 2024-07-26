using Blog.Core.DTOs.Account;
using Blog.Core.Security;
using Blog.Core.Services.Interfaces;
using Blog.DataLayer.Entities.Account;
using Blog.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Services.Implementation
{
    public class UserService : IUserService
    {
        #region Constructor
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }
        #endregion


        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetEntitiesQuery().ToListAsync();
        }

        public bool IsUserExistByEmail(string email) 
        { 
            return  _userRepository.GetEntitiesQuery().Any( s => s.Email == email.ToLower().Trim());
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if(IsUserExistByEmail(register.Email)) return RegisterUserResult.EmailExists;
            var user = new User
            {
                Email = register.Email.SanitizeText(),
                UserName = register.UserName,
                Password = _passwordHelper.EncodePasswordMd5(register.Email),
                CreateDate = DateTime.Now,
                EmailActiveCode = Guid.NewGuid().ToString()
            };

            await _userRepository.AddEntity(user);

            await _userRepository.SaveChanges();

           

            return RegisterUserResult.Success;

        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login/*, bool checkAdminRole = false*/)
        {
            var password = _passwordHelper.EncodePasswordMd5(login.Password);

            var user = await _userRepository.GetEntitiesQuery()
                .SingleOrDefaultAsync(s => s.Email == login.Email.ToLower().Trim() && s.Password == password);

            if (user == null) return LoginUserResult.IncorrectData;

            //if (!user.IsEmailActive) return LoginUserResult.NotActivated;

            //if (checkAdminRole)
            //{
            //    if (!await IsUserAdmin(user.Id))
            //    {
            //        return LoginUserResult.NotAdmin;
            //    }
            //}

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(s => s.Email == email.ToLower().Trim());
        }


        #region Dispose
        public void Dispose()
        {
            _userRepository?.Dispose();
        }


        #endregion
    }
}
