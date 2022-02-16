using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Bl.Dto
{
    public class BaseDto
    {
        public int Id { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
