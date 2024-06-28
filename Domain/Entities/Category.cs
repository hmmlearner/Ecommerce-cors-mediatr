using Domain.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category: AuditableEntity, IAggregateRoot
    {
        public string Name { get;  set; } 
        public int DisplayOrder { get; set; } 


        public Category(string name, int displayOrder) 
        {
            Name = name;
            DisplayOrder = displayOrder;
        }
        public Category(Guid id, string name, int displayOrder)
        {
            Id = id;
            Name = name;
            DisplayOrder = displayOrder;
        }

        public void Update(string name, int displayOrder)
        {
            if(!string.IsNullOrEmpty(name) && !Name.Equals(name)) Name = name;
            if(displayOrder > 0) DisplayOrder = displayOrder;
        }
    }
}
