using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Contracts
{
    public interface ISoftDelete
    {
        DateTime? DeletedOn { get; set; }
        Guid DeletedBy { get; set; }
    }
}
