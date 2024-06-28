using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Contracts
{
    public abstract class AuditableEntity : AuditableEntity<Guid>
    {
    }

    public abstract class AuditableEntity<T>: BaseEntity<T>, IAuditableEntity, ISoftDelete
    {
        public Guid CreatedBy { get; set ; }
        public Guid LastModifiedBy { get; set ; }
        public DateTime CreatedOn { get ; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid DeletedBy { get; set; }

        protected AuditableEntity()
        {
            CreatedOn = DateTime.UtcNow;
            LastModifiedOn = DateTime.UtcNow;
        }
    }

}
