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
    public class PostController : ControllerBase
    {

        private static List<Post> postList = new List<Post>();

        [HttpPost]
        public ActionResult CreatePost([FromBody] CreatePostDTO post, [FromHeader] string token = "")
        {
            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                PostRepository postRepository = new PostRepository();
                PostService postService = new PostService(postRepository);
                return Ok(postService.CreatePost(post));
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
        public ActionResult GetPost([FromHeader] string token = "")
        {
            List<ListAllPostsDTO> postsDTO;

            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                PostRepository postRepository = new PostRepository();
                PostService postService = new PostService(postRepository);

                List<Post> posts = postService.GetAllPosts();
                postsDTO = new List<ListAllPostsDTO>();

                posts.ForEach(posts =>
                {
                    ListAllPostsDTO postList = new ListAllPostsDTO();
                    postList.Id_post = posts.GetIdPost();
                    postList.Id_theme = posts.GetIdTheme();
                    postList.Title = posts.GetTitle();
                    postList.Resum = posts.GetResum();
                    postList.Body = posts.GetBody();
                    postList.Id_user = posts.GetIdUser();
                    postList.created = posts.GetCreate();
                    postsDTO.Add(postList);
                });
            }
            catch (BadRequest ex)
            {
                return BadRequest(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return Ok(postsDTO);
        }


        [HttpGet("{id}")]
        public ActionResult GetPostById(string id, [FromHeader] string token = "")
        {
            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                PostRepository postRepository = new PostRepository();
                PostService postService = new PostService(postRepository);

                Post postSave = postService.GetPostById(id);

                if (postSave != null)
                {
                    GetPostDTO postDTO = new GetPostDTO();

                    postDTO.Id_post = postSave.GetIdPost();
                    postDTO.Id_theme = postSave.GetIdTheme();
                    postDTO.Title = postSave.GetTitle();
                    postDTO.Resum = postSave.GetResum();
                    postDTO.Body = postSave.GetBody();
                    postDTO.Id_user = postSave.GetIdUser();
                    postDTO.created = postSave.GetCreate();
                    return Ok(postDTO);
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

            return Ok("Post não encontrado");
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteById(string id, [FromHeader] string token = "")
        {
            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                PostRepository postRepository = new PostRepository();
                PostService postService = new PostService(postRepository);

                bool postSave = postService.DeleteById(id);

                return Ok("Post deletado com sucesso");
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

        [HttpPut("{id}")]
        public ActionResult UpdateById(string id, [FromBody] CreatePostDTO post, [FromHeader] string token = "")
        {
            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                PostRepository postRepository = new PostRepository();
                PostService postService = new PostService(postRepository);

                Post postSave = postService.UpdateById(id, post);

                return Ok("Atualização de post realizada com sucesso");
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

        [HttpGet("themes")]
        public ActionResult GetPostByThemes([FromBody] string[] id)
        {
            List<ListAllPostsDTO> postsDTO;

            try
            {
                PostRepository postRepository = new PostRepository();
                PostService postService = new PostService(postRepository);

                List<Post> posts = postService.PostByTheme(id);
                postsDTO = new List<ListAllPostsDTO>();

                posts.ForEach(posts =>
                {
                    ListAllPostsDTO postList = new ListAllPostsDTO();
                    postList.Id_post = posts.GetIdPost();
                    postList.Id_theme = posts.GetIdTheme();
                    postList.Title = posts.GetTitle();
                    postList.Resum = posts.GetResum();
                    postList.Body = posts.GetBody();
                    postList.Id_user = posts.GetIdUser();
                    postList.created = posts.GetCreate();
                    postsDTO.Add(postList);
                });
            }
            catch (BadRequest ex)
            {
                return BadRequest(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return Ok(postsDTO);
        }
    }
}
