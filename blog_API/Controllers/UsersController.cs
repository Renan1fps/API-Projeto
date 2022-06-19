using blog_API.Dtos;
using blog_API.Errors;
using blog_API.Models;
using blog_API.Repository;
using blog_API.Services;
using blog_API.Utils;
using Microsoft.AspNetCore.Mvc;


namespace blog_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> userList = new List<User>();

        [HttpPost]
        public ActionResult CreateUser([FromBody] CreateUserDTO user, [FromHeader] string token = "")
        {
            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (user.IsAdmin && !isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                UserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);
                return Ok(userService.CreateUser(user));
            }
            catch (BadRequest ex)
            {
                return Unauthorized(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }
        }

        [HttpGet]
        public ActionResult GetUsers([FromHeader] string token = "")
        {
            List<ListAllUsersDTO> usersDTO;

            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                UserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);

                List<User> users = userService.GetAllUsers();
                usersDTO = new List<ListAllUsersDTO>();

                users.ForEach(user =>
                {
                    ListAllUsersDTO userList = new ListAllUsersDTO();
                    userList.Id = user.GetId();
                    userList.Name = user.GetName();
                    userList.Email = user.GetEmail();
                    userList.Password = user.GetPassword();
                    userList.IsAdmin = user.GetIsAdmin();
                    userList.created = user.GetCreate();
                    usersDTO.Add(userList);
                });
            }
            catch (BadRequest ex)
            {
                return Unauthorized(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return Ok(usersDTO);
        }


        [HttpGet("{id}")]
        public ActionResult GetUserById(string id, [FromHeader] string token = "")
        {
            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                UserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);

                User userSave = userService.GetUserById(id);

                if (userSave != null)
                {
                    GetUserDTO userDTO = new GetUserDTO();

                    userDTO.Id = userSave.GetId();
                    userDTO.Name = userSave.GetName();
                    userDTO.Email = userSave.GetEmail();
                    userDTO.Password = userSave.GetPassword();
                    userDTO.IsAdmin = userSave.GetIsAdmin();
                    userDTO.created = userSave.GetCreate();
                    return Ok(userDTO);
                }
            }
            catch (BadRequest ex)
            {
                return Unauthorized(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return null;
        }


        [HttpPost("auth")]
        public ActionResult UserAuthentication([FromBody] AuthDTO auth)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);

                string token = userService.UserAuthentication(auth.Email, auth.Password);

                if (token != null)
                {

                    return Ok(token);
                }
            }
            catch (BadRequest ex)
            {
                return BadRequest(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(string id)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);

                bool userSave = userService.DeleteById(id);

                return Ok(userSave);
            }
            catch (BadRequest ex)
            {
                return BadRequest(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateById(string id, [FromBody] CreateUserDTO user)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                UserService userService = new UserService(userRepository);

                User userSave = userService.UpdateById(id, user);

                return Ok("Usuário atualizado");
            }
            catch (BadRequest ex)
            {
                return BadRequest(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

        }

    }
}
