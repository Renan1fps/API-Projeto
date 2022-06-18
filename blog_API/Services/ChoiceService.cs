using blog_API.Dtos;
using blog_API.Models;
using blog_API.Repository;

namespace blog_API.Services {

    public class ChoiceService {

        private ChoicesRepository choiceRepository = null;

        public ChoiceService(ChoicesRepository choiceRepository) {
            this.choiceRepository = choiceRepository;
        }

        public string CreateChoice(string[] theme, string id) {

            for (int i = 0; i > 2; i++)
            {
                Choices choiceToSave = new Choices(theme[i], id);
                choiceToSave.GenerateId();

                this.choiceRepository.CreateChoices(choiceToSave); 

            }

            return "Escolhas salvas";

        }

        public List<Choices> GetAllChoices() { 
            return this.choiceRepository.GetAllChoices();
        }


    }
}
