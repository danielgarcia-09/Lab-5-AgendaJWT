using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Bl.Dto
{
    public class EventDto : BaseDto
    {
        public int UserId { get; set; }

        public DateTime EventDate { get; set; }

        public bool Completed { get; set; }
    }
}
