using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventService.DBContexts
{
    public class EventServiceContext: DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Event> Events { get; set; }
        public EventServiceContext(DbContextOptions<EventServiceContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person
                {    Id = 1,                
                    Name = "Sarish Paul",
                    DOB = Convert.ToDateTime("1980-01-19"),
                },
                new Person
                {
                    Id = 2,
                    Name = "Person 2",
                    DOB = Convert.ToDateTime("1998-11-01"),
                }
            );
        }
    }
}
