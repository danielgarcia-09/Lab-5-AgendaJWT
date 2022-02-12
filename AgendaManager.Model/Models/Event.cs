using System;

namespace AgendaManager.Model.Models
{
    public class Event : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime EventDate { get; set; }

        public bool Completed { get; set; }
    }
}
