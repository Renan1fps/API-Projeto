using blog_API.Dtos;
using blog_API.Models;
using blog_API.Repository;
using blog_API.Services;
using Microsoft.AspNetCore.Mvc;


namespace blog_API.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase {
        private static List<User> userList = new List<User>();

        [HttpPost]
        public string CreateUser([FromBody] CreateUserDTO user) {

            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);

            return userService.CreateUser(user);
        }

        [HttpGet]
        public List<User> GetUsers() {
            return UserRepository.GetAllUsers();
        }
    }
}
