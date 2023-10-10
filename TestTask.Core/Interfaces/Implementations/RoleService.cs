using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Interfaces.Abstractions;
using TestTask.Core.Models;
using TestTask.Data.Entities;
using TestTask.DataAccess.Abstractions;

namespace TestTask.Core.Interfaces.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> Repository;
        private readonly IMapper Mapper;

        public RoleService(IRepository<Role> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public RoleDto GetByName(string name)
        {
            return Mapper.Map<RoleDto>(Repository.FindBy(role => role.Name.Equals(name)));
        }
    }
}
