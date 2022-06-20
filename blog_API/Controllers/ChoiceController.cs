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
    public class ChoiceController : ControllerBase
    {

        private static List<Choices> choiceList = new List<Choices>();

        [HttpPost("{id}")]
        public ActionResult CreateChoice([FromBody] string[] themes, string id)
        {
            try
            {

                ChoicesRepository choiceRepository = new ChoicesRepository();
                ChoiceService choiceService = new ChoiceService(choiceRepository);
                return Ok(choiceService.CreateChoice(id, themes));
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
        public ActionResult GetChoices([FromHeader] string token = "")
        {
            List<ListAllChoicesDTO> choicesDTO;

            try
            {
                bool isAdmin = Authorize.HasPermissionAdmin(token);

                if (!isAdmin)
                {
                    throw new BadRequest("Não autorizado");
                }

                ChoicesRepository choicesRepository = new ChoicesRepository();
                ChoiceService choiceService = new ChoiceService(choicesRepository);

                List<Choices> choices = choiceService.GetAllChoices();
                choicesDTO = new List<ListAllChoicesDTO>();

                choices.ForEach(posts =>
                {
                    ListAllChoicesDTO choiceList = new ListAllChoicesDTO();
                    choiceList.Id_choice = posts.GetIdChoice();
                    choiceList.Id_theme = posts.GetIdTheme();
                    choiceList.Id_user = posts.GetIdUser();
                    choiceList.created = posts.GetCreate();
                    choicesDTO.Add(choiceList);
                });
            }
            catch (BadRequest ex)
            {
                return BadRequest(ex.GetMensagem());
            }
            catch (IntegrationException ex)
            {
                return BadRequest(ex.GetMensagem());
            }

            return Ok(choicesDTO);
        }

    }
}
