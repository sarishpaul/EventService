using EventService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventService.Repository
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
        Person GetPersonById(int PersonId);
        public Person GetPersonByNameAndDob(string name, DateTime dob);
        Person InsertPerson(Person Person);
        void UpdatePerson(Person Person);
        void Save();
    }
}
