using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Models;

namespace TestTask.Core.Interfaces.Abstractions
{
    public interface IUserService
    {
        Task CreateUser(UserDto userDto);
        Task UpdateUser(UserDto userDto);
        Task DeleteUser(Guid userId);
        IQueryable<UserDto> GetAllUsers();
        Task<UserDto> GetUser(Guid id);
    }
}
