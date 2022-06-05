using blog_API.Dtos;
using blog_API.Models;
using blog_API.Repository;

namespace blog_API.Services {
    public class UserService {

        private UserRepository userRepository = null;

        public UserService(UserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public string CreateUser(CreateUserDTO user) {
            User existsUser = this.userRepository.GetUserByEmail(user.Email);
           

            if (existsUser != null) {
                Console.WriteLine(existsUser.GetEmail());
                return "Usuário já existente"; // TODO: make a helper 400 request
            }

            User userToSave = new User(user.Name, user.Email, user.Password, user.IsAdmin);
            userToSave.CriptoPassword();
            userToSave.GenerateId();

            this.userRepository.CreateUser(userToSave);
            return "usuário criado";
        }
    }
}
