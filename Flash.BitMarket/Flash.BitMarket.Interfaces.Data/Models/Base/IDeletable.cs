using System;

namespace Flash.BitMarket.Interfaces.Data.Models
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
