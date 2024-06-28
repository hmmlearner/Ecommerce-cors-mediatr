﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Contracts
{
    public interface IAuditableEntity
    {   
        Guid CreatedBy { get; set; }
        Guid LastModifiedBy { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime? LastModifiedOn { get; set; }
    }
}
