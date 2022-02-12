using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Bl.Dto
{
    public class BaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
