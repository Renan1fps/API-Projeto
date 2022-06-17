using System.ComponentModel.DataAnnotations;

namespace blog_API.Models {
    public class Post {

        private string Id_post;
        private string Id_theme;
        private string title;
        private string resum;
        private string body;
        private string id_user;
        private DateTime Created;

        public Post(string title, string resum, string body)
        {
            this.title = title;
            this.resum = resum;
            this.body = body;
        }

        public Post(string id_theme, string title, string resum, string body, string id_user) {
            this.Id_theme = id_theme;
            this.title = title;
            this.resum = resum;
            this.body = body;
            this.id_user = id_user;
        }

        public Post(string id_post, string id_theme, string title, string resum, string body, string id_user, DateTime createdAt) {
            this.Id_post = id_post;
            this.Id_theme = id_theme;
            this.title = title;
            this.resum = resum;
            this.body = body;
            this.id_user = id_user;
            this.Created = createdAt;
        }

        public void GenerateId() {
            this.Id_post = Guid.NewGuid().ToString("N");
        }

        public string GetTitle() {
            return this.title;
        }

        public void SetTitle(string title) {
            this.title = title;
        }

        public string GetResum() {
            return this.resum;
        }

        public void SetResum(string resum) {
            this.resum = resum;
        }

        public string GetBody() {
            return this.body;
        }

        public void SetBody(string body) {
            this.body = body;
        }

        public string GetIdTheme() {
            return this.Id_theme;
        }

        public string GetIdUser() {
            return this.id_user;
        }

        public string GetIdPost()
        {
            return this.Id_post;
        }

        public DateTime GetCreate() {
            return this.Created;
        }

    }
}
