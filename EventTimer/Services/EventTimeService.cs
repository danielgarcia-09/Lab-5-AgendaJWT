using AgendaManager.Core.Model;
using AgendaManager.Model.Context;
using AgendaManager.Services.Service;
using EventTimer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTimer.Services
{
    public interface IEventTimeService
    {
        Task<bool> NotifyByEmail(int id);
    }

    public class EventTimeService : IEventTimeService
    {

        private readonly AgendaContext _context;

        private readonly IEmailService _emailService;

        public readonly TimeConfig _timeConfig;

        public EventTimeService(AgendaContext context, IEmailService emailService, IOptions<TimeConfig> timeConfig)
        {
            _context = context;
            _emailService = emailService;
            _timeConfig = timeConfig.Value;
        }

        public async Task<bool> NotifyByEmail(int id) {

            var user = await _context.Users.FindAsync(id);

            if (user is null) return false;

            var events = await _context.Events.Where(x=> x.UserId == user.Id).ToListAsync();
            
            var list = new List<DateTime>();
            foreach (var e in events )
            {
                list.Add(e.EventDate);
            }

            var query = from d in list
                        let val = Math.Abs(Convert.ToDouble((d - DateTime.Now).TotalMinutes.ToString()))
                        orderby val
                        select d;

            var closestDate = query.FirstOrDefault();

            var specificEvent = _context.Events.Where(x => x.EventDate == closestDate).FirstOrDefault();

            var reminderTime = closestDate.AddMinutes(-_timeConfig.TimeBeforeNotify);

            if ( DateTime.Compare(reminderTime, DateTime.Now) == 0  )
            {
                var emailModel = new EmailModel()
                {
                    Message = $"Reminder: At {closestDate.ToLongTimeString()} you have: {specificEvent.Name}",
                    Destinataries =
                    {
                        "20186648@itla.edu.do"
                    }
                };
                await _emailService.SendEmail(emailModel);
                return true;
            }

            return false;
        }
    }
}
