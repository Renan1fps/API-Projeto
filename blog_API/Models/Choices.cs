using System.ComponentModel.DataAnnotations;

namespace blog_API.Models
{
    public class Choices
    {

        private string IdChoice;
        private string IdTheme;
        private string IdUser;
        private DateTime Created;


        public Choices(string IdTheme, string IdUser)
        {
            this.IdTheme = IdTheme;
            this.IdUser = IdUser;
        }

        public Choices(string IdChoice, string IdTheme, string IdUser, DateTime createdAt)
        {
            this.IdChoice = IdChoice;
            this.IdTheme = IdTheme;
            this.IdUser = IdUser;
            this.Created = createdAt;
        }


        public void GenerateId()
        {
            this.IdChoice = Guid.NewGuid().ToString("N");
        }

        public string GetIdTheme()
        {
            return this.IdTheme;
        }

        public string GetIdUser()
        {
            return this.IdUser;
        }

        public string GetIdChoice()
        {
            return this.IdChoice;
        }

        public DateTime GetCreate()
        {
            return this.Created;
        }

    }
}
