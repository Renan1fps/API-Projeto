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
        public List<ListAllUsersDTO> GetUsers() {
            List<ListAllUsersDTO> usersDTO = new List<ListAllUsersDTO>();

            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);

            List<User> users = userService.GetAllUsers();

            users.ForEach(user => {
                ListAllUsersDTO userList = new ListAllUsersDTO();
                userList.Id = user.GetId();
                userList.Name = user.GetName();
                userList.Email = user.GetEmail();
                userList.Password = user.GetPassword();
                userList.IsAdmin = user.GetIsAdmin();
                userList.created = user.GetCreate();
                usersDTO.Add(userList);
            });

            return usersDTO;
        }


        [HttpGet("{id}")]
        public GetUserDTO GetUserById(string id) {

            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);

            User userSave = userService.GetUserById(id);

            if (userSave != null) {
                GetUserDTO userDTO = new GetUserDTO();

                userDTO.Id = userSave.GetId();
                userDTO.Name = userSave.GetName();
                userDTO.Email = userSave.GetEmail();
                userDTO.Password = userSave.GetPassword();
                userDTO.IsAdmin = userSave.GetIsAdmin();
                userDTO.created = userSave.GetCreate();
                return userDTO;
            }

            return null;
        }


        [HttpDelete("{id}")]
        public bool DeleteById(string id) {

            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);

            bool userSave = userService.DeleteById(id);     

            return userSave;
        }

    }
}
