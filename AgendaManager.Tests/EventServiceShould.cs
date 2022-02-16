using AgendaManager.Bl.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AgendaManager.Tests
{
    public class EventServiceShould : BaseTestService
    {
        private readonly ITestOutputHelper _output;

        public EventServiceShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task AddEvent()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            await _userService.Create(user);

            var @event = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Hector baby shower",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(3),
                Deleted = false
            };

            var result = await _eventService.Create(@event);
            
            Assert.NotNull(result);

            _output.WriteLine(result.Name);
        }

        [Fact]
        public async Task GetEvents()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            await _userService.Create(user);

            var event1 = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Hector baby shower",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(3),
                Deleted = false
            };

            var event2 = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Daniel's graduation",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddDays(2),
                Deleted = false
            };

            var event3 = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Israel bar mitzvah",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddMonths(1),
                Deleted = false
            };

            await _eventService.Create(event1);
            await _eventService.Create(event2);
            await _eventService.Create(event3);

            var allEvents = _eventService.Get();

            Assert.NotNull(allEvents);
        }

        [Fact]
        public async Task GetEventById()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            await _userService.Create(user);

            var @event = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Hector baby shower",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(3),
                Deleted = false
            };

            await _eventService.Create(@event);

            var result = await _eventService.GetById(2);

            Assert.NotNull(result);
            _output.WriteLine(result.Name);
        }

        [Fact]
        public async Task UpdateEvent()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            await _userService.Create(user);

            var @event = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Hector baby shower",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(3),
                Deleted = false
            };

            var addedEvent = await _eventService.Create(@event);

            var eventUpdate = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Hector's racing competition",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(6),
                Deleted = false
            };

            var updatedUser = await _eventService.Update(addedEvent.Id, eventUpdate);

            Assert.NotEqual(addedEvent, updatedUser);

        }

        [Fact]
        public async Task DeleteEvent()
        {
            var user = new UserDto
            {
                Id = 2,
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                Password = "string1234",
                Deleted = false
            };

            await _userService.Create(user);


            var @event = new EventDto
            {
                UserId = user.Id,
                Id = 2,
                Name = "Hector baby shower",
                StartDate = DateTime.Now.AddHours(1),
                EndDate = DateTime.Now.AddHours(3),
                Deleted = false
            };

            await _eventService.Create(@event);

            var isDeleted = await _eventService.Delete(@event.Id);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task HaveTimeConditionsWhenCreating()
        {
            var event1 = new EventDto
            {
                UserId = 1,
                Id = 2,
                Name = "Hector baby shower",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(3),
                Deleted = false
            };

            var result1 = await _eventService.Create(event1);

            Assert.Null(result1);

            var event2 = new EventDto
            {
                UserId = 1,
                Id = 2,
                Name = "baby",
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now,
                Deleted = false
            };

            var result2 = await _eventService.Create(event2);

            Assert.Null(result2);

            var event3 = new EventDto
            {
                UserId = 1,
                Id = 2,
                Name = "shower",
                StartDate = new DateTime(2022,11,10,10,10,10),
                EndDate = new DateTime(2022, 10, 10, 10, 10, 10),
                Deleted = false
            };

            var result3 = await _eventService.Create(event3);

            Assert.Null(result3);
        }
    }
}
