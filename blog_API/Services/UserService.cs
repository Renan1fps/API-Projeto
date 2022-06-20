using blog_API.Dtos;
using blog_API.Errors;
using blog_API.Models;
using blog_API.Repository;

namespace blog_API.Services
{
    public class UserService
    {

        private UserRepository userRepository = null;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public string CreateUser(CreateUserDTO user)
        {
            User existsUser = this.userRepository.GetUserByEmail(user.Email);


            if (existsUser != null)
            {
                return "Usuário já existente"; // TODO: make a helper 400 request
            }

            User userToSave = new User(user.Name, user.Email, user.Password, user.IsAdmin);
            userToSave.CriptoPassword();
            userToSave.GenerateId();

            this.userRepository.CreateUser(userToSave);
            return "usuário criado";
        }

        public List<User> GetAllUsers()
        {
            return this.userRepository.GetAllUsers();
        }

        public User GetUserById(string id)
        {

            User existsUser = this.userRepository.GetUserById(id);

            if (existsUser == null)
            {
                return null;
            }

            return existsUser;
        }

        public static bool PasswordCompare(string hash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public AuthResponseDTO UserAuthentication(string email, string pass)
        {
            User existsUser = this.userRepository.GetUserByEmailAndPass(email, pass);

            if (existsUser == null)
            {
                throw new BadRequest("invalid credentials");
            }

            if (PasswordCompare(existsUser.GetPassword(), pass))
            {
                var key = existsUser.GetIsAdmin() ? "admin" : "view";
                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                string hashKey = BCrypt.Net.BCrypt.HashPassword(key, salt);

                GetChoicesDTO choices = this.userRepository.GetChoicesByUserId(existsUser.GetId());
                GetUserDTO user = new GetUserDTO();
                user.Id = existsUser.GetId();
                user.IsAdmin = existsUser.GetIsAdmin();
                user.Name = existsUser.GetName();
                user.Email = existsUser.GetEmail();
                user.created = existsUser.GetCreate();

                AuthResponseDTO response = new AuthResponseDTO();
                response.user = user;
                response.token = hashKey;
                response.choices = choices;

                return response;
            }

            throw new BadRequest("invalid credentials");
        }

        public AuthResponseDTO UserReloadChoice(string id)
        {
            User existsUser = this.userRepository.GetUserById(id);

            if (existsUser == null)
            {
                throw new BadRequest("invalid credentials");
            }

            if (existsUser != null)
            {

                GetChoicesDTO choices = this.userRepository.GetChoicesByUserId(existsUser.GetId());
                GetUserDTO user = new GetUserDTO();
                user.Id = existsUser.GetId();
                user.IsAdmin = existsUser.GetIsAdmin();
                user.Name = existsUser.GetName();
                user.Email = existsUser.GetEmail();
                user.created = existsUser.GetCreate();

                AuthResponseDTO response = new AuthResponseDTO();
                response.user = user;
                response.token = " ";
                response.choices = choices;

                return response;
            }

            throw new BadRequest("invalid credentials");
        }

        public bool DeleteById(string id)
        {

            User existsUser = this.userRepository.GetUserById(id);

            if (existsUser == null)
            {
                return false;
            }

            bool sucess = this.userRepository.DeleteById(id);
            return sucess;
        }

        public User UpdateById(string id, CreateUserDTO user)
        {
            Console.WriteLine("service");
            User existsUser = this.userRepository.GetUserById(id);

            if (existsUser == null) {
                Console.WriteLine("nulo");
                return null;
            }
            Console.WriteLine("veio para fora");

            Console.WriteLine("Passou aqui depois do email");
            User userToUpdate = new User(user.Name, user.Email, user.Password, user.IsAdmin);
            userToUpdate.CriptoPassword();

            bool sucess = this.userRepository.UpdateById(id, userToUpdate);


            return sucess ? userToUpdate : null;
        }
    }
}
