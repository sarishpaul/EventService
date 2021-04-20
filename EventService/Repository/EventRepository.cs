using EventService.DBContexts;
using EventService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventService.Repository
{
    public class EventRepository: IEventRepository
    {
        public readonly EventServiceContext _dbContext;

        public EventRepository(EventServiceContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Event GetEventById(int EventId)
        {
            return _dbContext.Events.Find(EventId);
        }


        public IEnumerable<Event> GetEvents()
        {
            return _dbContext.Events.ToList();
        }

        public void InsertEvent(Event evt)
        {

            _dbContext.Events.Attach(evt);
            _dbContext.Entry(evt).State = EntityState.Added;
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateEvent(Event evt)
        {
            throw new NotImplementedException();
        }
    }
}
