namespace blog_API.Dtos {
    public class AuthResponseDTO {
        public string token { get; set; }
        public GetUserDTO user { get; set; } 
        
        public GetChoicesDTO choices { get; set; }
    }
}
