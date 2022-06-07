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
                return "Usuário já existente"; // TODO: make a helper 400 request
            }

            User userToSave = new User(user.Name, user.Email, user.Password, user.IsAdmin);
            userToSave.CriptoPassword();
            userToSave.GenerateId();

            this.userRepository.CreateUser(userToSave);
            return "usuário criado";
        }

        public List<User> GetAllUsers() { 
            return this.userRepository.GetAllUsers();
        }

        public User GetUserById(string id) {

            User existsUser = this.userRepository.GetUserById(id);

            if(existsUser == null) {
                return null;
            }

            return existsUser;
        }

        public bool DeleteById(string id) {

            User existsUser = this.userRepository.GetUserById(id);

            if (existsUser == null) {
                return false;
            }

            bool sucess = this.userRepository.DeleteById(id);
            return sucess;
        }

        public User UpdateById(string id, CreateUserDTO user) {

            User existsUser = this.userRepository.GetUserById(id);

            if (existsUser == null) {
                return null;
            }

            User existsemail = this.userRepository.GetUserByEmail(user.Email);

            if (existsemail != null) {
                return null; ;
            }

            User userToUpdate = new User(user.Name, user.Email, user.Password, user.IsAdmin);
            userToUpdate.CriptoPassword();
          
            bool sucess = this.userRepository.UpdateById(id, userToUpdate);


            return sucess ? userToUpdate : null;
        }
    }
}
