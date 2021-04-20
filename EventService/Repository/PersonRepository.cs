using EventService.DBContexts;
using EventService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventService.Repository
{
    public class PersonRepository: IPersonRepository
    {
        public readonly EventServiceContext _dbContext;
        public PersonRepository(EventServiceContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Person> GetPersons()
        {
            return _dbContext.Persons.ToList();
        }
        public Person GetPersonById(int PersonId)
        {
            return _dbContext.Persons.Find(PersonId);
        }
        public Person GetPersonByNameAndDob(string name, DateTime dob)
        {
            return _dbContext.Persons.FirstOrDefault(p => p.Name.ToLower()== name && p.DOB.Date == dob.Date);
        }
        public Person InsertPerson(Person Person)
        {
            _dbContext.Persons.Add(Person);
            Save();
            return Person;
        }
        public void UpdatePerson(Person Person)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
