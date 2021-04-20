using EventService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventService.Repository
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetEvents();
        Event GetEventById(int EventId);
        void InsertEvent(Event evt);
        void UpdateEvent(Event evt);
        void Save();
    }
}
