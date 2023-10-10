using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Models;

namespace TestTask.Core.Interfaces.Abstractions
{
    public interface IRoleService
    {
        RoleDto GetByName(string name);
    }
}
