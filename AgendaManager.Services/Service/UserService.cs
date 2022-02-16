using AgendaManager.Bl.Dto;
using AgendaManager.Model.Context;
using AgendaManager.Model.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaManager.Services.Service
{
    public interface IUserService : IBaseService<User,UserDto,AgendaContext>
    {
        Task<UserDto> IsUser(string email, string password);
    }
    public class UserService : BaseService<User, UserDto, AgendaContext>, IUserService
    {
        public UserService(AgendaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserDto> IsUser(string email, string password)
        {
            var user = await _dbSet.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();

            if (user is null) return null;

            return _mapper.Map<UserDto>(user);
        }

    }
}
