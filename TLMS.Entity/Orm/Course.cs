using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLMS.Entity.Orm
{
    class Course
    {
        public string CourseCode { get; set; }

        public string CourseTitle { get; set; }

        public string School { get; set; }

        public string ProgrammeLevel { get; set; }

        public string Programme { get; set; }

        public string CourseType { get; set; }

        public string CourseArea { get; set; }

        public string CourseUnit { get; set; }

        public string Remarks { get; set; }
    }
}
