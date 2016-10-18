using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLMS.Common
{
    public class AuditInfo
    {
        public LocalDateTime CreatedOn { get; set; }
        public LocalDateTime ModifiedOn { get; set; }
    }

    public class AuditInfo<T> : AuditInfo
    {
        public T CreatedBy { get; set; }
        public T ModifiedBy { get; set; }
    }
}
