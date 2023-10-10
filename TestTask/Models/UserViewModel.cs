using TestTask.Core.Models;
using TestTask.Data.Entities;

namespace TestTask.Models
{
    public class UserViewModel
    {
        public IEnumerable<UserDto> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
