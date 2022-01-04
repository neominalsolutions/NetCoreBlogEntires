using FluentValidation;
using NetCoreBlogEntires.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Validators
{
    public class ContactInputModelValidator:AbstractValidator<ContactInputModel>
    {
        public ContactInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("isim boş geçilemez");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-posta formatında giriniz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta alanı boş geçilemez");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu alanı boş geçilemez");
            RuleFor(x => x.MessageBody).NotEmpty().WithMessage("Mesaj alanı boş geçilemez");
            RuleFor(x => x.MessageBody).MaximumLength(200).WithMessage("200 karakter uzun bir mesaj girilemez");
            //RuleFor(x => x.Age).LessThan(18).GreaterThan(30).WithMessage("18-30 yaş aralığında olmalısınız");
            //RuleFor(x => x.Subject).Equal("Deneme").WithMessage("Subject alanı deneme olarak girilmemiş");
            //RuleFor(x => x.Subject).NotEqual("Deneme").WithMessage("Subject alanı deneme olarak girilmemiş");

            // NotEqual
            // NotNull gibi gibi başka validayon özellikleride mevcut

            //RuleFor(x => x.Subject).CreditCard().WithMessage("Kredi kart formatında değil");
            // match true false göre çalışır
            //RuleFor(x => x.Subject).Matches(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,15})").WithMessage("en az 1 adet harf 1 adet rakam 1 adet özel karakter 1 adet küçük 1 adet büyük harf içeren minimum 8 makismum 15 karakter uzunluğunda bir şifre giriniz");

            //RuleFor(x => x.Subject).Must(NameExist).WithMessage("Bu isim sistemde mevcut değildir");

        }

        private bool NameExist(string name)
        {
            string[] names = { "Ali", "Ahmet", "Can" };

            return names.Any(x=> x == name);
        }
    }
}
