using Auth.Application.DTOS;
using Auth.Application.Interface;
using Auth.Domain.Entities;
using Auth.Domain.Interface;
using Auth.Infrastructure.Services;

namespace Auth.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IUserRepository userRepo, IPasswordHasher passwordHasher, IJwtTokenGenerator tokenGenerator)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<string> RegisterAsync(UserRegisterDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                throw new ArgumentException("Username and password are required.");

            var existingUser = await _userRepo.GetByUsernameAsync(model.Username);
            if (existingUser != null)
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PasswordHash = _passwordHasher.HashPassword(model.Password)
            };

            await _userRepo.AddAsync(user);
            return _tokenGenerator.GenerateToken(user);
        }

        public async Task<string> LoginAsync(UserLoginDto model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                throw new ArgumentException("Invalid login request.");

            var user = await _userRepo.GetByUsernameAsync(model.Username);
            if (user == null || !_passwordHasher.VerifyPassword(model.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid username or password.");

            return _tokenGenerator.GenerateToken(user);
        }
    }
}
