using System;

namespace AgendaManager.Model.Models
{
    public interface IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
