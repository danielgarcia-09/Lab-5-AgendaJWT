using System;
using System.Collections.Generic;

namespace AgendaManager.Model.Models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
