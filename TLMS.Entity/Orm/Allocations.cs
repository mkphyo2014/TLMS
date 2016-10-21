using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLMS.Entity.Orm
{
    public class Allocations
    {

        public Course Course { get; set; }
        public Faculty Faculty { get; set; }

        public string CourseCode { get; set; }

        public string CourseTitle { get; set; }

        public string Level { get; set; }

        public string Programme { get; set; }

        public string CourseArea { get; set; }

        public int CourseUnits { get; set; }

        public string AcadYear { get; set; }

        public string Term { get; set; }

        public string InstructorName { get; set; }

        public int TeachingLoad { get; set; }

        public string Remarks { get; set; }
    }


}