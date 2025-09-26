using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; }//usedId
        public DateTime? CreatedOn { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime? LastModificationOn { get;set; }
        public bool IsDeleted { get; set; }
    }
}
