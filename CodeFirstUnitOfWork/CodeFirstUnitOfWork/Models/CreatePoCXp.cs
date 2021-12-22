using System;

namespace CodeFirstUnitOfWork.Models
{
    public class CreatePoCXp
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
    }
}
