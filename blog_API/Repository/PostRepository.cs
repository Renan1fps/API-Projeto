using blog_API.Dtos;
using blog_API.Models;
using MySql.Data.MySqlClient;
using blog_API.Errors;

namespace blog_API.Repository
{
    public class PostRepository
    {

        static readonly string url = @"server=projeto.cyvycyex4cnc.us-east-1.rds.amazonaws.com;uid=root;pwd=pedro.123;database=bd_article_dev;ConnectionTimeout=2";
        static readonly MySqlConnection connection = new MySqlConnection(url);

        public static void OpenConection()
        {
            connection.Open();
        }

        public List<Post> GetAllPosts()
        {
            try
            {
                string queryString = "SELECT * FROM tb_posts";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();
                List<Post> lista = new List<Post>();

                while (data.Read())
                {
                    string id_post = data.GetString(0);
                    string id_theme = data.GetString(1);
                    string resum = data.GetString(2);
                    string body = data.GetString(3);
                    string id_user = data.GetString(4);
                    DateTime createdAt = data.GetDateTime(5);
                    string title = data.GetString(6);
                    Post post = new Post(id_post, id_theme, title, resum, body, id_user, createdAt);
                    lista.Add(post);
                }
                data.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }

        public void CreatePost(Post post)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = "INSERT INTO tb_posts (id_post, id_theme, title, resum, body, created_by)" +
                $"VALUES ( '{post.GetIdPost()}', '{post.GetIdTheme()}', '{post.GetTitle()}', '{post.GetResum()}', '{post.GetBody()}' , '{post.GetIdUser()}')";

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

        public Post GetPostByTitle(string title)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_posts WHERE title = '{title}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
                    string id_post = data.GetString(0);
                    string id_theme = data.GetString(1);
                    string resum = data.GetString(2);
                    string body = data.GetString(3);
                    string id_user = data.GetString(4);
                    DateTime createdAt = data.GetDateTime(5);
                    string titleSave = data.GetString(6);
                    Post post = new Post(id_post, id_theme, titleSave, resum, body, id_user, createdAt);
                    data.Close();
                    return post;
                }

                data.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }

        public Post GetPostByID(string id)
        {

            MySqlCommand command = null;
            try
            {
                string queryString = $"SELECT * FROM tb_posts WHERE id_post = '{id}'";

                command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();

                if (data.Read())
                {
                    string id_post = data.GetString(0);
                    string id_theme = data.GetString(1);
                    string resum = data.GetString(2);
                    string body = data.GetString(3);
                    string id_user = data.GetString(4);
                    DateTime createdAt = data.GetDateTime(5);
                    string title = data.GetString(6);
                    Post post = new Post(id_post, id_theme, title, resum, body, id_user, createdAt);
                    data.Close();
                    return post;
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
                string queryString = $"DELETE FROM tb_posts WHERE id_post = '{id}'";

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

        public bool UpdateById(string id, Post post)
        {
            MySqlCommand command = null;
            try
            {
                string queryString = $"UPDATE tb_posts SET title = '{post.GetTitle()}', resum = '{post.GetResum()}', body = '{post.GetBody()}' WHERE id_post = '{id}'";

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
        public List<Post> PostByTheme(string[] id)
        {
            try
            {
                string queryString = $"SELECT * FROM tb_posts WHERE id_theme in ('{id[0]}', '{id[1]}', '{id[2]}') ";

                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader data = command.ExecuteReader();
                List<Post> lista = new List<Post>();


                while (data.Read())
                {
                    string id_post = data.GetString(0);
                    string id_theme = data.GetString(1);
                    string resum = data.GetString(2);
                    string body = data.GetString(3);
                    string id_user = data.GetString(4);
                    DateTime createdAt = data.GetDateTime(5);
                    string title = data.GetString(6);
                    Post post = new Post(id_post, id_theme, title, resum, body, id_user, createdAt);
                    lista.Add(post);
                }
                data.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw new IntegrationException(ex.Message);
            }
        }
    }

}

