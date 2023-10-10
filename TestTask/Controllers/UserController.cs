using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TestTask.Core.Interfaces.Abstractions;
using TestTask.Core.Models;
using TestTask.Models;
using TestTask.Models.Enums;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly IRoleService RoleService;
        private readonly ILogger<UserController> Logger;

        public UserController(IUserService userService, IRoleService roleService, ILogger<UserController> logger)
        {
            UserService = userService;
            RoleService = roleService;
            Logger = logger;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers(UserSortState sortOrder, int page = 1)
        {
            try
            {
                int pageSize = 3;
                var users = UserService.GetAllUsers();

                users = sortOrder switch
                {
                    UserSortState.Id => users.OrderBy(x => x.Id),
                    UserSortState.NameAsc => users.OrderBy(x => x.Name),
                    UserSortState.NameDesc => users.OrderByDescending(x => x.Name),

                    UserSortState.AgeAsc => users.OrderBy(x => x.Name),
                    UserSortState.AgeDesc => users.OrderByDescending(x => x.Name),
                    UserSortState.RoleName => users.OrderBy(x => x.Roles.First().Name),
                    UserSortState.RoleId => users.OrderBy(x => x.Roles.First().Id)
                };


                var count = await users.CountAsync();
                var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                var result = new UserViewModel
                {
                    PageViewModel = new PageViewModel(count, page, pageSize),
                    Users = items
                };

                return Ok(users);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest(404);
            }

        }

        [Authorize(Roles = "Admin, Support, SuperAdmin")]
        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var result = await UserService.GetUser(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("CreateUser")]
        public UserDto CreateUser()
        {
            return new UserDto();
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            try
            {
                if (userDto is null)
                {
                    return BadRequest(412);
                }

                if (userDto.Name is null || !userDto.Name.IsNormalized())
                {
                    ModelState.AddModelError("Name", "Is not valid");
                    return BadRequest(412);
                }

                if (userDto.Email is null || !EmailIsNormalized(userDto.Email))
                {
                    ModelState.AddModelError("Email", "Is not valid");
                    return BadRequest(412);
                }

                if (userDto.Age < 0)
                {
                    ModelState.AddModelError("Age", "Shuld be > 0");
                    return BadRequest(412);
                }

                await UserService.CreateUser(userDto);
                return Ok(await UserService.GetUser(userDto.Id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest(404);
            }

        }

        [Authorize]
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser (UserDto userDto)
        {
            try
            {
                await UserService.UpdateUser(userDto);
                return Ok(await UserService.GetUser(userDto.Id));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest(204);
            }

        }

        [Authorize(Roles = "Admin, Support, SuperAdmin")]
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser (Guid userId)
        {
            try
            {
                await UserService.DeleteUser(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest(204);
            }

        }

        [Authorize(Roles = "Admin, Support, SuperAdmin")]
        [HttpPost]
        [Route("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRole(RoleModel roleModel)
        {
            try
            {
                if (roleModel is null)
                {
                    return BadRequest();
                }

                var user = await UserService.GetUser(roleModel.UserId);
                var userRole = RoleService.GetByName(roleModel.Name);

                if (user.Roles.Contains(userRole))
                {
                    return BadRequest(500);
                }

                user.Roles.Clear();
                user.Roles.Add(userRole);
                await UserService.UpdateUser(user);
                return Ok(user);
            }

            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return BadRequest(500);
            }

        }

        private bool EmailIsNormalized(string email) 
        {
           return  MailAddress.TryCreate(email, out var address);
        }
    }
}
