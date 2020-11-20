using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GroupClass.Core.Models
{
    [Table("member")]
    public class Member:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int Userid { get; set; }
        [ForeignKey(nameof(Class))]
        public int Classid { get; set; }

        public virtual User User { get; set; }
        public virtual Class Class { get; set; }
    }
}
