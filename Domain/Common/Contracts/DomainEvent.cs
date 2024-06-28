using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Contracts
{
    public abstract class DomainEvent : IEvent
    {

        public DateTime OccurredOn { get; protected set; }

        public DomainEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }
}
