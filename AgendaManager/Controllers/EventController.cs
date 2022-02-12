using AgendaManager.Bl.Dto;
using AgendaManager.Core.Model;
using AgendaManager.Model.Context;
using AgendaManager.Model.Models;
using AgendaManager.Services.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaManager.Controllers
{
    public class EventController : BaseController<Event, EventDto, AgendaContext>
    {
        private readonly IEmailService _emailService;

        private readonly IEventService _eventService;
        public EventController(IEventService eventService, IEmailService emailService) : base(eventService)
        {
            _emailService = emailService;
            _eventService = eventService;
        }

        public override async Task<IActionResult> Get()
        {
            var userId = this.User.FindFirst(x => x.Type == "UserId").Value;
            
            var events = await _eventService.GetEvents(int.Parse(userId));

            return Ok(events);
        }

        public override async Task<IActionResult> Create(EventDto dto)
        {
            var userId = this.User.FindFirst(x => x.Type == "UserId").Value;
            
            dto.UserId = int.Parse(userId);
            
            var created = await _eventService.Create(dto);

            if (created != null) return Ok(created);

            var error = new
            {
                message = "Not Possible, other event in same date"
            };
            return BadRequest(error);
        }
        
        public override async Task<IActionResult> Update(int id, EventDto dto)
        {
            dto.UserId = int.Parse(this.User.FindFirst(x => x.Type == "UserId").Value);

            var updated = await _eventService.Update(id, dto);

            if (updated != null) return Ok(updated);

            var error = new
            {
                message = "Check the Request"
            };
            return BadRequest(error);
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteUsers(EmailModel emailModel)
        {
            var isSent = await _emailService.SendEmail(emailModel);

            if (isSent) return Ok();

            return BadRequest();
        } 
    }
}
