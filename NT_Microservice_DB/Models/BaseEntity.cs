﻿namespace NT_Microservice_DB.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

    }
}
