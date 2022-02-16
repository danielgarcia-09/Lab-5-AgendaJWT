using System;
using System.Collections.Generic;

namespace AgendaManager.Bl.Dto
{
    public class EventDto : BaseDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
