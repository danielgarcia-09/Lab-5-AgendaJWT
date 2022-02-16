using System;

namespace AgendaManager.Model.Models
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }

    }

    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }
    }
}
