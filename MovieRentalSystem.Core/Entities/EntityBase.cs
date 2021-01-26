using System;

namespace MovieRentalSystem.Core.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = LastChangedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastChangedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
