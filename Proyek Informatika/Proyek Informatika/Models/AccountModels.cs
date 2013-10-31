using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyek_Informatika.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Saat Ini")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Panjang {0} minimal {2} karakter.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password Baru")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmasi Password Baru")]
        [Compare("NewPassword", ErrorMessage = "Password baru dan konfirmasinya tidak sesuai.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Ingat saya?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Panjang {0} minimal {2} karakter.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmasi password")]
        [Compare("Password", ErrorMessage = "Password dan konfirmasinya tidak sesuai.")]
        public string ConfirmPassword { get; set; }
    }
}
