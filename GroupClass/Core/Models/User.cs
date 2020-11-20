using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Models
{
    [Table("user")]
    public class User:BaseEntity
    {
        public string UserRole { get; set; }

        [Required (ErrorMessage ="Email Gereklidir.")]
        [StringLength(20, ErrorMessage = "Email 20 karakterden uzun olamaz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Gereklidir.")]
        [StringLength(50,ErrorMessage ="Şifre en fazla 50 karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(20, ErrorMessage = "İsim en fazla 20 karakter olmalıdır.")]
        public string Firstname { get; set; }

        [StringLength(20, ErrorMessage = "Soyad en fazla 20 karakter olmalıdır.")]
        public string Lastname { get; set; }
        public string ActivationCode { get; set; }

        [MaxLength(100,ErrorMessage ="Profil image url 100 karakterden uzun olamaz.")]
        public string ProfileImageUrl { get; set; }

        [StringLength(9, ErrorMessage = "Okul Numarası en fazla 9 karakter olmalıdır.")]
        public string SchoolNumber { get; set; }



    }
}
