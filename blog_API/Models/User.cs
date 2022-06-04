﻿using System.ComponentModel.DataAnnotations;

namespace blog_API.Models {
    public class User {

        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
