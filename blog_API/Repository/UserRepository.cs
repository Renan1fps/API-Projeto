using blog_API.Models;
using MySql.Data.MySqlClient;

namespace blog_API.Repository {
    public class UserRepository {

        static readonly string url = @"server=projeto.cyvycyex4cnc.us-east-1.rds.amazonaws.com;uid=root;pwd=pedro.123;database=bd_article_dev;ConnectionTimeout=2";
        static readonly MySqlConnection connection = new MySqlConnection(url);

        public static void OpenConection() {
            connection.Open();
        }

        public static List<User> GetAllUsers() {
            try {
                string queryString = "SELECT * FROM users";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();
                List<User> lista = new List<User>();

                while (data.Read()) {
                    string id = data.GetString(0);
                    string name = data.GetString(1);
                    string email = data.GetString(2);
                    DateTime createdAt = data.GetDateTime(4);
                    User user = new User() { Id = id, Name = name, Email = email, Created = createdAt };
                    lista.Add(user);
                }
                data.Close();
                return lista;
            }
            catch (Exception ex) {
                Console.WriteLine(ex); // TODO: make custom exception
                return null;
            }
        }

        public static void CreateUser(User user) {
            MySqlCommand command = null;
            try {
                string queryString = "INSERT INTO users (id, name, passowrd, is_admin)" +
                $"VALUES ( '{"8"}', '{user.Name}', '{user.Password}', {user.IsAdmin})"; // TODO: id aleatório

                command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
            finally {
                if (command != null) command.Dispose();
            }
        }
    }
}
