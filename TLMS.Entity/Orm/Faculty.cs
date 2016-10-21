using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using ServiceStack.DataAnnotations;

namespace TLMS.Entity.Orm
{
    public class Faculty
    {
        [PrimaryKey, ForeignKey(typeof(Allocation), OnDelete = "NO ACTION")]
        public string AllocationSub { get; set; }

        [StringLength(50)]
        public int FacultyId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string School { get; set; }

        [StringLength(30)]
        public string Rank { get; set; }

        [StringLength(100)]
        public string DisciplineArea { get; set; }

        [StringLength(100)]
        public string DisciplineSubArea { get; set; }

        [StringLength(20)]
        public string FacultyCategory { get; set; }

        public LocalDateTime? ContractStartDt { get; set; }

        public LocalDateTime? ContractEndDt { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
    }
}
