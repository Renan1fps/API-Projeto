using blog_API.Dtos;
using blog_API.Models;
using blog_API.Repository;

namespace blog_API.Services
{

    public class ChoiceService
    {

        private ChoicesRepository choiceRepository = null;
        private UserRepository userRepository = null;

        public ChoiceService(ChoicesRepository choiceRepository)
        {
            this.choiceRepository = choiceRepository;
        }

        public string CreateChoice(string id, string[] theme)
        {

            for (int i=0; i<theme.Length; i++)  
            {
                Choices choiceToSave = new Choices(theme[i], id);
                choiceToSave.GenerateId();
                Console.WriteLine("Aqui");

                this.choiceRepository.CreateChoices(choiceToSave);

            }

            return "Escolhas não salvas";

        }

        public List<Choices> GetAllChoices()
        {
            return this.choiceRepository.GetAllChoices();
        }


    }
}
