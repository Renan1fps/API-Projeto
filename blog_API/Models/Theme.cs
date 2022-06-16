using System.ComponentModel.DataAnnotations;



namespace blog_API.Models
{
    public class Theme
    {
        private string Id;
        private string Name;
        private DateTime Created;


        public Theme(string name)
        {
            this.Name = name;            
        }

        public Theme(string id, string name, DateTime createdAt)
        {
            this.Id = id;
            this.Name = name;
            this.Created = createdAt;
        }

        public void GenerateId()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }

        public string GetName()
        {
            return this.Name;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public string GetId()
        {
            return this.Id;
        }

        public DateTime GetCreate()
        {
            return this.Created;
        }













    }
}
