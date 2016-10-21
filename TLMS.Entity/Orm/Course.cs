using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace TLMS.Entity.Orm
{
    public class Course
    {
        [PrimaryKey, ForeignKey(typeof(Allocation), OnDelete = "NO ACTION")]
        public string AllocationSub { get; set; }

        [StringLength(10)]
        public string CourseCd { get; set; }

        [StringLength(100)]
        public string CourseTitle { get; set; }

        [StringLength(10)]
        public string School { get; set; }

        [StringLength(10)]
        public string ProgrammeLevel { get; set; }

        [StringLength(10)]
        public string Programme { get; set; }

        [StringLength(20)]
        public string CourseType { get; set; }

        [StringLength(100)]
        public string CourseArea { get; set; }

        public decimal CourseUnit { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
    }
}
