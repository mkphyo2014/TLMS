using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using ServiceStack.DataAnnotations;

namespace TLMS.Entity.Orm
{
    public class Term
    {
        [PrimaryKey, ForeignKey(typeof(Allocation), OnDelete = "NO ACTION")]
        public string AllocationSub { get; set; }

        [StringLength(40)]
        public string TermDescription
        {
            get { return this.TermName + " " + this.SessionName; }
        }
        public int Id { get; set; }

        [StringLength(10)]
        public string TermName { get; set; }

        [StringLength(30)]
        public string SessionName { get; set; }

        public LocalDateTime? BeginDt { get; set; } //Change to LocalDate
        public LocalDateTime? EndDt { get; set; }

    }
}

