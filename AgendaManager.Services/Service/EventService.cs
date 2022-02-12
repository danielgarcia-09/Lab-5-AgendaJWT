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
            var events = await GetEvents(dto.UserId);

            var isSame = events.Where(x => x.EventDate == dto.EventDate).ToList();
            
            if (isSame.Count == 0)
            {
                var isEarlierThanNow = DateTime.Compare(dto.EventDate, DateTime.Now);

                if (isEarlierThanNow > 0)
                {

                    var created = await base.Create(dto);
                    
                    return created;
                }

                return null;
            }

            return null;
        }

        public override async Task<EventDto> Update(int id, EventDto dto)
        {
            var isEvent = await GetById(id);

            if(isEvent != null)
            {
                if (isEvent.Completed) return null;

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
