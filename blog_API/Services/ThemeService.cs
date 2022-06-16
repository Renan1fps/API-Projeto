using blog_API.Dtos;
using blog_API.Models;
using blog_API.Repository;


namespace blog_API.Services
{
    public class ThemeService
    {

        private ThemeRepository themeRepository = null;

        public ThemeService(ThemeRepository themeRepository)
        {
            this.themeRepository = themeRepository;
        }

        public string CreateTheme(CreateThemeDTO theme)
        {
            Theme existsTheme = this.themeRepository.GetThemeByName(theme.Name);


            if (existsTheme != null)
            {
                return "Usuário já existente"; // TODO: make a helper 400 request
            }

            Theme themeToSave = new Theme(theme.Name);
            themeToSave.GenerateId();

            this.themeRepository.CreateTheme(themeToSave);
            return "Tema criado";
        }

        public List<Theme> GetAllThemes()
        {
            return this.themeRepository.GetAllThemes();
        }

        public Theme GetThemeById(string id)
        {

            Theme existsTheme = this.themeRepository.GetThemeById(id);

            if (existsTheme == null)
            {
                return null;
            }

            return existsTheme;
        }

        public bool DeleteById(string id)
        {

            Theme existsTheme = this.themeRepository.GetThemeById(id);

            if (existsTheme == null)
            {
                return false;
            }

            bool sucess = this.themeRepository.DeleteById(id);
            return sucess;
        }

    }
}
