using blog_API.Models;
using blog_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace blog_API.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase {
        private static List<User> userList = new List<User>();

        [HttpPost]
        public void CreateUser([FromBody] User user) {
            UserRepository.CreateUser(user);
        }

        [HttpGet]
        public List<User> GetUsers() {
            return UserRepository.GetAllUsers();
        }
    }
}
