using AgendaManager.Bl.Mapping;
using AgendaManager.Model.Context;
using AgendaManager.Services.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace AgendaManager.Tests
{
    public class BaseTestService : IDisposable
    {
        private DbContextOptions<AgendaContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<AgendaContext>()
                .UseInMemoryDatabase(databaseName: "Agenda")
                .Options;
        }

        private AgendaContext _context;

        private IMapper _mapper;

        protected readonly IUserService _userService;
        protected readonly IEventService _eventService;

        public BaseTestService()
        {
            _context = new AgendaContext(CreateOptions());
            _mapper = new MapperConfiguration(x => x.AddProfile<AgendaProfile>())
                .CreateMapper();
            _userService = new UserService(_context, _mapper);
            _eventService = new EventService(_context, _mapper);
        }
        public void Dispose()
        {
            
        }
    }
}
