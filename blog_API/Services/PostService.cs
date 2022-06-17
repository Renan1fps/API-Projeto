using blog_API.Dtos;
using blog_API.Models;
using blog_API.Repository;

namespace blog_API.Services {
    public class PostService {

        private PostRepository postRepository = null;

        public PostService(PostRepository postRepository) {
            this.postRepository = postRepository;
        }

        public string CreatePost(CreatePostDTO post) {
            Post existsPost = this.postRepository.GetPostByTitle(post.Title);
           

            if (existsPost != null) {
                return "Post já existente"; // TODO: make a helper 400 request
            }

            Post postToSave = new Post(post.IdTheme,post.Title, post.Resum, post.Body, post.IdUser);
            postToSave.GenerateId();

            this.postRepository.CreatePost(postToSave);
            return "Post criado";
        }

        public List<Post> GetAllPosts() { 
            return this.postRepository.GetAllPosts();
        }

        public Post GetPostById(string id) {

            Post existsPost = this.postRepository.GetPostByID(id);

            if(existsPost == null) {
                return null;
            }

            return existsPost;
        }

        public bool DeleteById(string id) {

            Post existsPost = this.postRepository.GetPostByID(id);

            if (existsPost == null) {
                return false;
            }

            bool sucess = this.postRepository.DeleteById(id);
            return sucess;
        }

        public Post UpdateById(string id, CreatePostDTO post) {

            Post existsPost = this.postRepository.GetPostByID(id);

            if (existsPost == null) {
                return null;
            }

            Post existsTitle = this.postRepository.GetPostByTitle(post.Title);

            if (existsTitle != null) {
                return null; ;
            }

            Post postToUpdate = new Post(post.Title, post.Resum, post.Body);
          
            bool sucess = this.postRepository.UpdateById(id, postToUpdate);


            return sucess ? postToUpdate : null;
        }

        public List<Post> PostByTheme(string[] id)
        {
            return this.postRepository.PostByTheme(id);
        }
    }
}
