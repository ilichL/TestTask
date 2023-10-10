using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Data.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public List<Role> Roles { get; set; }
    }
}
