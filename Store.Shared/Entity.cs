﻿namespace Store.Shared
{
    public abstract class Entity 
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            LastUpdated = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public DateTime LastUpdated { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
