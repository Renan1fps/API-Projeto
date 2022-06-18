using blog_API.Dtos;
using blog_API.Errors;
using blog_API.Models;
using blog_API.Repository;
using blog_API.Services;
using blog_API.Utils;
using Microsoft.AspNetCore.Mvc;


namespace blog_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ThemesController : ControllerBase
    {


        private static List<Theme> themeList = new List<Theme>();


        [HttpPost]
        public ActionResult CreateTheme([FromBody] CreateThemeDTO theme, [FromHeader] string token = "")
        {
            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                ThemeRepository themeRepository = new ThemeRepository();
                ThemeService themeService = new ThemeService(themeRepository);
                return Ok(themeService.CreateTheme(theme));
            }
            catch (BadRequest ex)
            {
                return Unauthorized(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }
        }

        [HttpGet]
        public ActionResult GetThemes()
        {
            List<ListAllThemesDTO> themesDTO;

            try
            {

                ThemeRepository themeRepository = new ThemeRepository();
                ThemeService themeService = new ThemeService(themeRepository);

                List<Theme> themes = themeService.GetAllThemes();
                themesDTO = new List<ListAllThemesDTO>();

                themes.ForEach(theme =>
                {
                    ListAllThemesDTO themesList = new ListAllThemesDTO();
                    themesList.Id = theme.GetId();
                    themesList.Name = theme.GetName();
                    themesList.created = theme.GetCreate();
                    themesDTO.Add(themesList);
                });
            }
            catch (BadRequest ex)
            {
                return Unauthorized(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return Ok(themesDTO);
        }


        [HttpGet("{id}")]
        public ActionResult GetThemeById(string id)
        {
            try
            {
                ThemeRepository themeRepository = new ThemeRepository();
                ThemeService themeService = new ThemeService(themeRepository);

                Theme themeSave = themeService.GetThemeById(id);

                if (themeSave != null)
                {
                    GetThemeDTO themeDTO = new GetThemeDTO();

                    themeDTO.Id = themeSave.GetId();
                    themeDTO.Name = themeSave.GetName();
                    themeDTO.created = themeSave.GetCreate();
                    return Ok(themeDTO);
                }
            }
            catch (BadRequest ex)
            {
                return BadRequest(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return null;
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteById(string id, [FromHeader] string token = "")
        {

            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                ThemeRepository themeRepository = new ThemeRepository();
                ThemeService themeService = new ThemeService(themeRepository);

                bool themeSave = themeService.DeleteById(id);

                return Ok(themeSave);
            }
            catch (BadRequest ex)
            {
                return Unauthorized(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

        }
    }
}
