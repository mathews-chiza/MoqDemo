
using DemoLibrary.Models;

namespace DemoLibrary.Logic
{
    public interface IPersonProcessor
    {
        PersonModel CreatePerson(string firstName, string lastName, string heightText);

        List<PersonModel> LoadPeople();

        void SavePerson(PersonModel person);

        void UpdatePerson(PersonModel person);
    }
}
