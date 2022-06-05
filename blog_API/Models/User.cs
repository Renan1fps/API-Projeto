using System.ComponentModel.DataAnnotations;

namespace blog_API.Models {
    public class User {

        private string Id;
        private string Name;
        private string Email;
        private string Password;
        private bool IsAdmin;
        private DateTime Created;


        public User(string name, string email, string password, bool isAdmin) {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsAdmin = isAdmin;
        }

        public User(string id, string name, string email, string password, bool isAdmin, DateTime createdAt) {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.IsAdmin = isAdmin;
            this.Created = createdAt;
        }

        public void CriptoPassword() {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(this.Password, salt);

            this.Password = hashPassword;
        }

        public void GenerateId() {
            this.Id = Guid.NewGuid().ToString("N");
        }

        public string GetName() {
            return this.Name;
        }

        public void SetName(string name) {
            this.Name = name;
        }

        public string GetEmail() {
            return this.Email;
        }

        public void SetEmail(string email) {
            this.Email = email;
        }

        public string GetPassword() {
            return this.Password;
        }

        public void SetPassword(string password) {
            this.Password = password;
        }

        public bool GetIsAdmin() {
            return this.IsAdmin;
        }

        public void SetIsAdmin(bool isAdmin) {
            this.IsAdmin = isAdmin;
        }

        public string GetId() {
            return this.Id;
        }

        public DateTime GetCreate() {
            return this.Created;
        }

    }
}
