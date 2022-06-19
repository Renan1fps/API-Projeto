using blog_API.Dtos;
using blog_API.Models;
using MySql.Data.MySqlClient;
using blog_API.Errors;

namespace blog_API.Repository
{
    public class UserRepository
    {

        static readonly string url = @"server=projeto.cyvycyex4cnc.us-east-1.rds.amazonaws.com;uid=root;pwd=pedro.123;database=bd_article;ConnectionTimeout=2";
        static readonly MySqlConnection connection = new MySqlConnection(url);

        public static void OpenConection()
        {
            connection.Open();
        }

        public List<User> GetAllUsers()
        {
            try
            {
                string queryString = "SELECT * FROM tb_users";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();
                List<User> lista = new List<User>();

                while (data.Read())
                {
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
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }

        }

        public void CreateUser(User user)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = "INSERT INTO tb_users (id_user, name_user, email, password, is_admin)" +
                $"VALUES ( '{user.GetId()}', '{user.GetName()}', '{user.GetEmail()}', '{user.GetPassword()}', {user.GetIsAdmin()})";

                command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
            finally
            {
                if (command != null) command.Dispose();
            }
        }

        public User GetUserByEmail(string email)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_users WHERE email = '{email}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
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
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }
        public User GetUserByEmailAndPass(string email, string pass)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_users WHERE email = '{email}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
                    string id = data.GetString(0);
                    string name = data.GetString(1);
                    string emailSave = data.GetString(2);
                    string passwordSave = data.GetString(3);
                    bool isAdmin = data.GetBoolean(4);
                    DateTime createdAt = data.GetDateTime(5);
                    User user = new User(id, name, emailSave, passwordSave, isAdmin, createdAt);
                    data.Close();
                    return user;
                }

                data.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }

        public User GetUserById(string id)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_users WHERE id_user = '{id}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
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
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }

        public bool DeleteById(string id)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"DELETE FROM tb_users WHERE id_user = '{id}'";

                command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
            finally
            {
                if (command != null) command.Dispose();
            }
        }

        public bool UpdateById(string id, User user)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"UPDATE tb_users SET name_user = '{user.GetName()}', email = '{user.GetEmail()}', password = '{user.GetPassword()}' WHERE id_user = '{id}'";

                command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
            finally
            {
                if (command != null) command.Dispose();
            }
        }

        public GetChoicesDTO GetChoicesByUserId(string id) {
            MySqlCommand command = null;
            try {
                string queryString = $"select tc.* from tb_choices tc left join tb_users tu ON tu.id_user = tc.id_user where tu.id_user = '{id}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                GetChoicesDTO choices = new GetChoicesDTO();
                string[] choicesArray = new string[3];
                int count = 0;

                while (data.Read()) {
                    string idThema = data.GetString(1);
                    choicesArray[count]= idThema;
                    count++;
                }
                data.Close();
                choices.IdChoices = choicesArray;
                return choices;
            }
            catch (Exception ex) {
                throw new IntegrationException(ex.Message);
            }
        }


    }
}
