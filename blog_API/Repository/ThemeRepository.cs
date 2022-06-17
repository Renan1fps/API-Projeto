using blog_API.Dtos;
using blog_API.Models;
using MySql.Data.MySqlClient;
using blog_API.Errors;


namespace blog_API.Repository
{

    public class ThemeRepository
    {
        static readonly string url = @"server=projeto.cyvycyex4cnc.us-east-1.rds.amazonaws.com;uid=root;pwd=pedro.123;database=bd_article_dev;ConnectionTimeout=2";
        static readonly MySqlConnection connection = new MySqlConnection(url);

        public static void OpenConection()
        {
            connection.Open();
        }


        public List<Theme> GetAllThemes()
        {
            try
            {
                string queryString = "SELECT * FROM tb_themes";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();
                List<Theme> lista = new List<Theme>();

                while (data.Read())
                {
                    string id = data.GetString(0);
                    string name = data.GetString(1);
                    DateTime createdAt = data.GetDateTime(2);
                    Theme theme = new Theme(id, name, createdAt);
                    lista.Add(theme);
                }
                data.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }

        public void CreateTheme(Theme theme)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = "INSERT INTO tb_themes (id_theme, name_theme)" +
                $"VALUES ( '{theme.GetId()}', '{theme.GetName()}')";

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

        public Theme GetThemeById(string id)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_themes WHERE id_theme = '{id}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
                    string idSave = data.GetString(0);
                    string name = data.GetString(1);
                    DateTime createdAt = data.GetDateTime(2);
                    Theme theme = new Theme(idSave, name, createdAt);
                    data.Close();
                    return theme;
                }

                data.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }

        public Theme GetThemeByName(string name_theme)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_themes WHERE name_theme = '{name_theme}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
                    string id = data.GetString(0);
                    string name = data.GetString(1);
                    DateTime createdAt = data.GetDateTime(2);
                    Theme theme = new Theme(name);
                    data.Close();
                    return theme;
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
                string queryString = $"DELETE FROM tb_themes WHERE id_theme = '{id}'";

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


    }


}
