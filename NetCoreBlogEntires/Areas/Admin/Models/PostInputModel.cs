using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Areas.Admin.Models
{
    public class PostInputModel
    {
        [Required(ErrorMessage ="Makale başlığı boş geçilemez")]
        [MaxLength(50,ErrorMessage ="En fazla 50 karakter giriniz")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Makalenin özetini boş geçmeyiniz")]
        [MinLength(150,ErrorMessage ="En az 150 karakterlik bir özet giriniz")]
        [MaxLength(300, ErrorMessage = "En fazla 300 karakterlik bir özet giriniz")]
        public string ShortContent { get; set; }

        // Html Content

        [MinLength(300, ErrorMessage = "En az 300 karakterlik bir içerik giriniz")]
        public string Content { get; set; }

        // Tagleri Split edeceğiz

        public string Tags { get; set; }

        [Required(ErrorMessage ="Makale kategorisi boş geçilemez")]
        public string CategoryId { get; set; }
        public string AuthorName { get; set; }



    }
}
