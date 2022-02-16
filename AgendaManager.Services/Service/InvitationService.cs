using AgendaManager.Bl.Dto;
using AgendaManager.Core.Model;
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
    public interface IInvitationService : IBaseService<Invitation, InvitationDto, AgendaContext>
    {
        Task<bool> InviteByEmail(InvitationDto dto);
        Task<bool> SendRemainder();
    }
    public class InvitationService : BaseService<Invitation, InvitationDto, AgendaContext>, IInvitationService
    {

        private readonly IEmailService _emailService;

        public InvitationService(IEmailService emailService, AgendaContext context, IMapper mapper) : base(context, mapper)
        {
            _emailService = emailService;
        }

        public override async Task<InvitationDto> Create(InvitationDto dto)
        {

            var @event = await _context.Set<Event>().FindAsync(dto.EventId);

            if (@event is null) return null;

            dto.Name = @event.Name;

            var added = await base.Create(dto);

            await InviteByEmail(added);

            return added;
        }

        public async Task<bool> InviteByEmail(InvitationDto dto)
        {
            var @event = _context.Events
                .Where(x => x.Id == dto.EventId)
                .Include("User")
                .FirstOrDefault();

            if (@event is null) return false;

            var emailModel = new EmailModel
            {
                Message = $"Greetings {@event.User.Name}, you are invited to this event: {@event.Name}, right now your answer is none, please let us know",
                Destinataries = new List<string>()
                {
                    @event.User.Email
                }
            };

            return await _emailService.SendEmail(emailModel);
        }

        public async Task<bool> SendRemainder()
        {
            var events = await _context.Events
                    .Where(@event => @event.StartDate >= DateTime.Now.AddMinutes(10)
                    && @event.StartDate <= DateTime.Now.AddMinutes(20))
                    .Include(u => u.User)
                    .ToListAsync();

            foreach (var @event in events)
            {
                var emailModel = new EmailModel
                {
                    Message = $"Today's remainder: {@event.Name} at {@event.StartDate.ToShortTimeString()}",
                    Destinataries = new List<string>()
                        {
                            @event.User.Email
                        }
                };
                await _emailService.SendEmail(emailModel);
            }
            return true;
        }
    }
}
