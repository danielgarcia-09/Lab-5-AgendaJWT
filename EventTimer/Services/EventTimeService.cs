using AgendaManager.Core.Model;
using AgendaManager.Model.Context;
using AgendaManager.Model.Models;
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
        Task Notify();
    }

    public class EventTimeService : IEventTimeService
    {

        private readonly IInvitationService _invitationService;
        public EventTimeService(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        public async Task Notify()
        {
            await _invitationService.SendRemainder();
        }
    }
}
