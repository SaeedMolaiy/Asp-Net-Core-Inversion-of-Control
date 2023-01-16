using AutoDependencyInjection.Attributes;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    // [Singleton]
    // [Transient]
    [Scoped]
    public class PersonRepository : IPersonRepository
    {
        private readonly SampleDbContext _dbContext;

        public PersonRepository(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HashSet<Person> GetPeople()
        {
            return new HashSet<Person>(_dbContext.People);
        }

        public Person GetPerson(int id)
        {
            return _dbContext.People.Find(id);
        }

        public void AddPerson(Person person)
        {
            _dbContext.People.Add(person);
            _dbContext.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            _dbContext.Update(person);
            _dbContext.SaveChanges();
        }

        public void RemovePerson(int id)
        {
            var person = GetPerson(id);
            RemovePerson(person);
        }

        public void RemovePerson(Person person)
        {
            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
        }

    }
}
