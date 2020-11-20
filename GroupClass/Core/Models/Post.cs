using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Models
{
    [Table("post")]
    public class Post:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int Userid { get; set; }

        [ForeignKey(nameof(Class))]
        public int Classid { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        
        public virtual  User User { get; set; }
        public virtual Class Class { get; set; }
    }
}
