using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.Interfaces.Abstractions;
using TestTask.Core.Models;
using TestTask.Data;
using TestTask.Data.Entities;
using TestTask.DataAccess.Abstractions;

namespace TestTask.Core.Interfaces.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> UserRepository;
        private readonly IMapper Mapper;
        public UserService(IRepository<User> userRepository, IMapper mapper) 
        {
            UserRepository = userRepository;
            Mapper = mapper;
        }

        public async Task CreateUser(UserDto userDto)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Age = userDto.Age,
                Email = userDto.Email,
                Roles = Mapper.Map<List<Role>>(userDto.Roles)
            };

           await UserRepository.Create(user);
        }

        public async Task UpdateUser(UserDto userDto)
        {
           await UserRepository.Update(Mapper.Map<User>(userDto));
        }

        public async Task DeleteUser (Guid userId)
        {
            await UserRepository.Delete(userId);
        }

        public IQueryable<UserDto> GetAllUsers()
        {
            var result = UserRepository.Get()
                .Include(r => r.Roles)
                .ToListAsync();
            return Mapper.Map<IQueryable<UserDto>>(result);
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            return Mapper.Map<UserDto>(await UserRepository.GetById(id));
        }

    }
}
