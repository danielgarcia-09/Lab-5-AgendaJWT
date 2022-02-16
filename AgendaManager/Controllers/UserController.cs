using AgendaManager.Bl.Dto;
using AgendaManager.Model.Context;
using AgendaManager.Model.Models;
using AgendaManager.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgendaManager.Controllers
{
    public class UserController : BaseController<User, UserDto, AgendaContext>
    {
        public UserController(IUserService service) : base(service)
        {
        }
    }
}
