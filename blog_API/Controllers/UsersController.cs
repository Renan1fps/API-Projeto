using blog_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog_API.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase {
        private static List<User> userList = new List<User>();

        [HttpPost]
        public void CreateUser([FromBody] User user) {

            Console.WriteLine(user.Id);
            userList.Add(user);
        }
    }
}
