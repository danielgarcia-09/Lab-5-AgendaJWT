using AgendaManager.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Model.Models
{
    public class Invitation : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public int @EventId { get; set; }
        public Event @Event { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
