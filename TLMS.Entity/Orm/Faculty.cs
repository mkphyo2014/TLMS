using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLMS.Entity.Orm
{
    class Faculty
    {
        public string Name { get; set; }

        public string School { get; set; }

        public string Rank { get; set; }

        public string DisciplineArea { get; set; }

        public string DisciplineSubArea { get; set; }

        public string FacultyCategory { get; set; }

        public Date ContractStartDt { get; set; }

        public Date ContractEndDt { get; set; }

        public string Remarks { get; set; }
    }
}
