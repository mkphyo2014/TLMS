using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace TLMS.Entity.Orm
{
    public class Allocation
    {
        [PrimaryKey]
        public string Sub { get; set; }
        [Reference]
        public Term Term { get; set; }
        [Reference]
        public Course Course { get; set; }
        [Reference]
        public Faculty Faculty { get; set; }
        [StringLength(10)]
        public string SectionCd { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }
        
    }


}