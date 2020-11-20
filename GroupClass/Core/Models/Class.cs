using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Models
{
    [Table("class")]
    public class Class:BaseEntity
    {
        [Required(ErrorMessage ="İsim Gerekli.")]
        [StringLength(50,ErrorMessage ="Sınıf ismi en fazla 50 karakter olabilir.")]
        public string Name { get; set; }
        [Required]
        public string AlphaNumericCode { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "Eğitim yılı en fazla 11 karakter olabilir.")]
        public string EducationYear { get; set; }
        public virtual List<Member> Members { get; set; }
    }
}
