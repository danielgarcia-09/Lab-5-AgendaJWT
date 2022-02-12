using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Bl.Dto
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }
    }
}
