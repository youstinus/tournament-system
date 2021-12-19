using System;

namespace tournament.Infrastructure.DataBase.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime Created { get; set; }
    }
}
