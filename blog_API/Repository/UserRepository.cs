using blog_API.Dtos;
using blog_API.Models;
using MySql.Data.MySqlClient;

namespace blog_API.Repository {
    public class UserRepository {

        static readonly string url = @"server=projeto.cyvycyex4cnc.us-east-1.rds.amazonaws.com;uid=root;pwd=pedro.123;database=bd_article_dev;ConnectionTimeout=2";
        static readonly MySqlConnection connection = new MySqlConnection(url);

        public static void OpenConection() {
            connection.Open();
        }

        public List<User> GetAllUsers() {
            try {
                string queryString = "SELECT * FROM users";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();
                List<User> lista = new List<User>();

                while (data.Read()) {
                    string id = data.GetString(0);
                    string name = data.GetString(1);
                    string emailSave = data.GetString(2);
                    string passwordSave = data.GetString(3);
                    bool isAdmin = data.GetBoolean(4);
                    DateTime createdAt = data.GetDateTime(5);
                    User user = new User(id, name, emailSave, passwordSave, isAdmin, createdAt);
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

        public void CreateUser(User user) {
            MySqlCommand command = null;
            try {
                string queryString = "INSERT INTO users (id, name, email, passowrd, is_admin)" +
                $"VALUES ( '{user.GetId()}', '{user.GetName()}', '{user.GetEmail()}', '{user.GetPassword()}', {user.GetIsAdmin()})";

                command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) {
                Console.WriteLine(ex);  // TODO: make custom exception
            }
            finally {
                if (command != null) command.Dispose();
            }
        }

        public User GetUserByEmail(string email) {
            MySqlCommand command = null;
            try {
                string queryString = $"SELECT * FROM users WHERE email = '{email}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read()) {
                    string id = data.GetString(0);
                    string name = data.GetString(1);
                    string emailSave = data.GetString(2);
                    string passwordSave = data.GetString(3);
                    bool isAdmin = data.GetBoolean(4);
                    DateTime createdAt = data.GetDateTime(5);
                    User user = new User(name, emailSave, passwordSave, isAdmin);
                    data.Close();
                    return user;
                }

                data.Close();
                return null;
            }
            catch (Exception ex) {
                Console.WriteLine(ex);  // TODO: make custom exception
                return null;
            }
        }

        public User GetUserById(string id) {
            MySqlCommand command = null;
            try {
                string queryString = $"SELECT * FROM users WHERE id = '{id}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read()) {
                    string idSave = data.GetString(0);
                    string name = data.GetString(1);
                    string emailSave = data.GetString(2);
                    string passwordSave = data.GetString(3);
                    bool isAdmin = data.GetBoolean(4);
                    DateTime createdAt = data.GetDateTime(5);
                    User user = new User(name, emailSave, passwordSave, isAdmin);
                    data.Close();
                    return user;
                }

                data.Close();
                return null;
            }
            catch (Exception ex) {
                Console.WriteLine(ex);  // TODO: make custom exception
                return null;
            }
        }

        public bool DeleteById(string id) {
            MySqlCommand command = null;
            try {
                string queryString = $"DELETE FROM users WHERE id = '{id}'";

                command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
                return true;
            }

            catch (Exception ex) {
                Console.WriteLine(ex);  // TODO: make custom exception
                return false;
            }

            finally {
                if (command != null) command.Dispose();
            }
        }

        public bool UpdateById(string id, User user) {
            MySqlCommand command = null;
            try {
                string queryString = $"UPDATE users SET name = '{user.GetName()}', email = '{user.GetEmail()}', passowrd = '{user.GetPassword()}' WHERE id = '{id}'";

                command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
                return true;
            }

            catch (Exception ex) {
                Console.WriteLine(ex);  // TODO: make custom exception
                return false;
            }

            finally {
                if (command != null) command.Dispose();
            }
        }

    }
}
