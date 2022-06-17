namespace blog_API.Dtos {
    public class GetPostDTO {
        public string Id_post { get; set; }
        public string Id_theme { get; set; }
        public string Title { get; set; }
        public string Resum { get; set; }
        public string Body { get; set; }
        public string Id_user { get; set; }
        public DateTime created { get; set; }
    }
}
