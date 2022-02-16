using System.Collections.Generic;

namespace AgendaManager.Model.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
    }
}
