using System;

namespace Flash.BitMarket.Interfaces.Data.Models
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
