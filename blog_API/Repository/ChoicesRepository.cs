using blog_API.Dtos;
using blog_API.Models;
using MySql.Data.MySqlClient;
using blog_API.Errors;

namespace blog_API.Repository
{
    public class ChoicesRepository
    {

        static readonly string url = @"server=projeto.cyvycyex4cnc.us-east-1.rds.amazonaws.com;uid=root;pwd=pedro.123;database=bd_article;ConnectionTimeout=2";
        static readonly MySqlConnection connection = new MySqlConnection(url);

        public static void OpenConection()
        {
            connection.Open();
        }

        public List<Choices> GetAllChoices()
        {
            try
            {
                string queryString = "SELECT * FROM tb_choices";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();
                List<Choices> lista = new List<Choices>();

                while (data.Read())
                {
                    string id_choice = data.GetString(0);
                    string id_theme = data.GetString(1);
                    string id_user = data.GetString(2);
                    DateTime createdAt = data.GetDateTime(3);
                    Choices choice = new Choices(id_choice, id_theme, id_user, createdAt);
                    lista.Add(choice);
                }
                data.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }

        public void CreateChoices(Choices choice)
        {
            MySqlCommand command = null;
            try
            {

                string queryString = "INSERT INTO tb_choices (id_choice, id_theme, id_user)" +
                $"VALUES ( '{choice.GetIdChoice()}', '{choice.GetIdTheme()}', '{choice.GetIdUser()}')";


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

        /*
        public Choices GetChoiceByID(string id)
        {

            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_choices WHERE id_post = '{id}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
                    string id_choice = data.GetString(0);
                    string id_theme = data.GetString(1);
                    string id_user = data.GetString(2);
                    DateTime createdAt = data.GetDateTime(3);
                    Choices choice = new Choices(id_choice, id_theme, id_user, createdAt);
                    data.Close();
                    return choice;
                }

                data.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }
        */

    }

}

