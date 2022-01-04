using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Models
{

    // dotnet add package FluentValidation.AspNetCore


    // Jquery unobtrusive ile client side validation kontrollü form yapalım
    // captcha yapalım
    // FluentValidation ile farklı bir validayon kütüphanesi kullanarak validayon yapalım
    // Automapper ile select viewleri otomatik olarak mapleyelim
    public class ContactInputModel
    {
        public string Name { get; set; }

        //[EmailAddress(ErrorMessage ="E-Posta formatında giriş yapınız")]
        //[Required(ErrorMessage ="E-Posta alanı boş Geçilemez")]
        public string Email { get; set; }

        //[Required(ErrorMessage ="Konu alanı boş geçilemez")]
        public string Subject { get; set; }

        //[Required(ErrorMessage ="Mesaj alanı boş geçilemez")]
        //[RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,15})")]
        public string Message { get; set; }

        public int Age { get; set; }


    }
}
