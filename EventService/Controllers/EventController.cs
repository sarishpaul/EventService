using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Common.Enums;
using Common.MessageQueue;
using Common.Utilities;
using EventService.MessageQueue;
using EventService.Models;
using EventService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Common.DependencyInjection.Utilities;

namespace EventService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventRepository _eventRepository;
        private readonly IPersonRepository _personRepository;
        public EventController(IEventRepository eventRepository, IPersonRepository personRepository)
        {
            _eventRepository = eventRepository;
            _personRepository = personRepository;
        }
        // GET: api/Event
        [HttpGet]
        public IActionResult Get()
        {

            var Events = _eventRepository.GetEvents();
            return new OkObjectResult(Events);
        }

        // GET: api/Event/5
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult Get(int id)
        {
            var evt = _eventRepository.GetEventById(id);
            return new OkObjectResult(evt);
        }

        // POST: api/Event
        [HttpPost]
        public IActionResult Post([FromBody] Event evt)
        {

            using (var scope = new TransactionScope())
            {
                Person person = _personRepository.GetPersonByNameAndDob(evt.Person.Name, evt.Person.DOB);
                if(person == null)
                {
                    person = _personRepository.InsertPerson(new Person() { Name = evt.Person.Name, DOB = evt.Person.DOB });
                }
                evt.Person = person;
                _eventRepository.InsertEvent(evt);
                scope.Complete();
                EventPayload ev = new EventPayload() { Name = evt.Name, PersonDOB = evt.Person.DOB.ToString(), PersonName = evt.Person.Name, TimeStamp = evt.TimeStamp };
                var ss = DependencyInjection.GetService<IMessageQueue>();
                ss.SendMessage(Utils.SerializeObject<EventPayload>(ev), "EventQueue", "Event");
                return CreatedAtRoute(routeName: "GetById", routeValues: new { id = evt.Id }, value: evt);
            }
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Event evt)
        {
            if (evt != null)
            {
                using (var scope = new TransactionScope())
                {
                    _eventRepository.UpdateEvent(evt);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
