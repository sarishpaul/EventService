using Common.MessageQueue;
using Common.Utilities;
using EventService.Models;
using EventService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.MessageQueue
{
    public class MessageProcessor
    {
        private IMessageQueue messageQueue;
        private IPersonRepository _personRepository;
        private IEventRepository _eventRepository;
        public MessageProcessor(IMessageQueue messageQueue, IPersonRepository personRepository, IEventRepository eventRepository)
        {
            this.messageQueue = messageQueue;
            this._personRepository = personRepository;
            this._eventRepository = eventRepository;
        }

        public void ProcessMessage(string message, IDictionary<string, object> headers)
        {

            try
            {
                if (headers != null && headers.ContainsKey("RequestType"))
                {
                    switch (Encoding.UTF8.GetString((byte[])headers["RequestType"]))
                    {
                        case "Person":

                            Person person = Utils.DeSerializeObject<Person>(message);
                            if (person != null)
                            {
                                Person personInDB = _personRepository.GetPersonByNameAndDob(person.Name, person.DOB);
                                if (personInDB == null)
                                {
                                    person = _personRepository.InsertPerson(new Person() { DOB = person.DOB, Name = person.Name });
                                }
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
