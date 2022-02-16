using System.Collections.Generic;

namespace AgendaManager.Bl.Dto
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
