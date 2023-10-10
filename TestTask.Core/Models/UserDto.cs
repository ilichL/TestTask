using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Data.Entities;

namespace TestTask.Core.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public List<RoleDto> Roles { get; set; }
    }


}
