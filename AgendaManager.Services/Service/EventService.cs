using AgendaManager.Bl.Dto;
using AgendaManager.Model.Context;
using AgendaManager.Model.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaManager.Services.Service
{
    public interface IEventService : IBaseService<Event, EventDto, AgendaContext>
    {
        Task<List<EventDto>> GetEvents(int id);

        Task<List<EventDto>> GetEventsByUserId(int id);
    }
    public class EventService : BaseService<Event, EventDto, AgendaContext>, IEventService
    {

        public EventService(AgendaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<EventDto>> GetEvents(int id)
        {
            var events = await _dbSet.Where(x => x.UserId == id).ToListAsync();
            return _mapper.Map<List<EventDto>>(events);
        }

        public override async Task<EventDto> Create(EventDto dto)
        {
            var TimeConditions = Get()
                .Where(x => x.StartDate >= dto.EndDate)
                .Where(x => x.StartDate <= dto.StartDate)
                .Where(x => x.EndDate >= dto.EndDate)
                .Where(x => x.EndDate <= dto.StartDate)
                .Where(x => x.UserId == dto.UserId)
                .Any();

            if (TimeConditions) return null;

            if (dto.StartDate < DateTime.Now || dto.EndDate < DateTime.Now) return null;

            if (dto.StartDate > dto.EndDate ) return null;

            return await base.Create(dto);
                    
        }

        public override async Task<EventDto> Update(int id, EventDto dto)
        {
            var isEvent = await GetById(id);

            if(isEvent != null)
            {
                if (isEvent.EndDate < DateTime.Now) return null;

                return await base.Update(id, dto);
            }
            return null;
        }

        public async Task<List<EventDto>> GetEventsByUserId(int id)
        {
            var events = await _dbSet.Where(x => x.UserId == id).ToListAsync();
            return _mapper.Map<List<EventDto>>(events);
        }
    }
}
