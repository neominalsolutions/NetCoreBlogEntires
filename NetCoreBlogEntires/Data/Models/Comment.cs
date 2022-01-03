using System;
using System.Globalization;
using System.Threading;

namespace NetCoreBlogEntires.Data.Models
{
    public class Comment
    {
        public string Id { get; private set; }
        public string CommentBy { get; private set; }
        public string Text { get; private set; }
        public DateTime PublishDate { get; private set; }


        public Comment(string commentBy, string text)
        {
            Id = Guid.NewGuid().ToString();
            PublishDate = DateTime.Now;
            SetCommentBy(commentBy);
            SetText(text);
        }

        private void SetCommentBy(string commentBy)
        {
            // login olmadan da yorum atılabilsin diye bu şekilde işaretledik.
            if (string.IsNullOrEmpty(commentBy))
            {
                CommentBy = "Anonim Hesap";
            }
            else
            {
                // bu arkadaş helper olacak.
                // baş harfini büyük yapar. sistemin dil ayarına göre bu işlemi yapar.

                // farklı dillere göre operasyon yapsın diye aşağıdaki kod ile dil ayarı değiştirebiliriz.
                //       System.Threading.Thread.CurrentThread.CurrentCulture =
                //System.Globalization.CultureInfo.CreateSpecificCulture("tr-TR");

                // dil değiştirmek ile ilgili bir örnek de buraya katalım.

                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;

                CommentBy = textInfo.ToTitleCase(commentBy.Trim());
            }
           
        }

        private void SetText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("Yorum kısmı boş geçilemez");
            }

            Text = text.Trim();
        }



    }
}