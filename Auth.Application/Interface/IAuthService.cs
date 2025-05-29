using Auth.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Interface
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDto model);
        Task<string> LoginAsync(UserLoginDto model);
    }
}
