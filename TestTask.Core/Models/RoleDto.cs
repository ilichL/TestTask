using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data.Entities;

namespace TestTask.Core.Models
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
