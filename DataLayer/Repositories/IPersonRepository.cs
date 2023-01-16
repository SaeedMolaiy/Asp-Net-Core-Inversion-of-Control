using AutoDependencyInjection.Attributes;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    [Injectable]
    public interface IPersonRepository
    {
        HashSet<Person> GetPeople();
        Person GetPerson(int id);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void RemovePerson(int id);
        void RemovePerson(Person person);
    }
}
