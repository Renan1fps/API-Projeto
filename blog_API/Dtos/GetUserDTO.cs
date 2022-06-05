namespace blog_API.Dtos {
    public class GetUserDTO {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime created { get; set; }
    }
}
