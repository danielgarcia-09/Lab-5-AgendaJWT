
namespace AgendaManager.Model.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }
    }
}
