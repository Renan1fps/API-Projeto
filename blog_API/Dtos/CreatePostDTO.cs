namespace blog_API.Dtos {
    public class CreatePostDTO {

        public string IdTheme { get; set; }
        public string Title { get; set; }
        public string Resum { get; set; }
        public string Body { get; set; }
        public string IdUser { get; set; }

    }
}
