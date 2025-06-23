using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReqResIntegration.Core.Models;

namespace ReqResIntegration.Core.Interfaces;

public interface IExternalUserService
{
    Task<UserDto> GetUserByIdAsync(int userId);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
}

