using blog_API.Dtos;
using blog_API.Errors;
using blog_API.Models;
using blog_API.Repository;
using blog_API.Services;
using Microsoft.AspNetCore.Mvc;


namespace blog_API.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class ThemesController : ControllerBase {
        
        
        private static List<Theme> themeList = new List<Theme>();


        [HttpPost]
        public ActionResult CreateTheme([FromBody] CreateThemeDTO theme) {
            try {
                ThemeRepository themeRepository = new ThemeRepository();
                ThemeService themeService = new ThemeService(themeRepository);
                return Ok(themeService.CreateTheme(theme));
            }
            catch (BadRequest ex) {
                BadRequest();
                return BadRequest(ex.GetMensagem());
            }
        }

        [HttpGet]
        public ActionResult GetThemes() {
            List<ListAllThemesDTO> themesDTO;

            try {
                ThemeRepository themeRepository = new ThemeRepository();
                ThemeService themeService = new ThemeService(themeRepository);

                List<Theme> themes = themeService.GetAllThemes();
                themesDTO = new List<ListAllThemesDTO>();

                themes.ForEach(theme => {
                    ListAllThemesDTO themesList = new ListAllThemesDTO();
                    themesList.Id = theme.GetId();
                    themesList.Name = theme.GetName();
                    themesList.created = theme.GetCreate();
                    themesDTO.Add(themesList);
                });
            }
            catch (BadRequest ex){
                return BadRequest(ex.GetMensagem());
            }

            return Ok(themesDTO);
        }


        [HttpGet("{id}")]
        public GetThemeDTO GetThemeById(string id) {

            ThemeRepository themeRepository = new ThemeRepository();
            ThemeService themeService = new ThemeService(themeRepository);

            Theme themeSave = themeService.GetThemeById(id);

            if (themeSave != null) {
                GetThemeDTO themeDTO = new GetThemeDTO();

                themeDTO.Id = themeSave.GetId();
                themeDTO.Name = themeSave.GetName();
                themeDTO.created = themeSave.GetCreate();
                return themeDTO;
            }

            return null;
        }


        [HttpDelete("{id}")]
        public bool DeleteById(string id) {

            ThemeRepository themeRepository = new ThemeRepository();
            ThemeService themeService = new ThemeService(themeRepository);

            bool themeSave = themeService.DeleteById(id);

            return themeSave;
        }
    }
}
