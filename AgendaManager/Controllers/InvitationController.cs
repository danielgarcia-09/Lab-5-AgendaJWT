using AgendaManager.Bl.Dto;
using AgendaManager.Model.Context;
using AgendaManager.Model.Models;
using AgendaManager.Services.Service;

namespace AgendaManager.Controllers
{
    public class InvitationController : BaseController<Invitation, InvitationDto, AgendaContext>
    {
        public InvitationController(IInvitationService service) : base(service)
        {
        }
    }
}
