﻿using NetCoreBlogEntires.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Models
{
    // post nesnesi comment ve tag içişn aggregate'tir.
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }

        public string AuthorName { get; set; }
        public string ShortContent { get; set; }
        public string CategoryId { get; set; }


        public Post()
        {

        }


        public Post(string title, string content,string shortContent, string categoryId, string authorName)
        {
            Id = Guid.NewGuid().ToString();
            CategoryId = categoryId;
            PublishDate = DateTime.Now;
            SetTitle(title);
            SetContent(content);
            SetShortContent(shortContent);
            SetAuthorName(authorName);
        }


        // Navigation Properties
        public Category Category { get; set; }

        private List<Tag> _tags = new List<Tag>();

        public IReadOnlyCollection<Tag> Tags => _tags;

        private List<Comment> _comments = new List<Comment>();
        public IReadOnlyCollection<Comment> Comments => _comments;

        private void SetAuthorName(string author)
        {
            if(string.IsNullOrEmpty(author))
            {
                // yazar ismi vermek istemezse anonim bir hesaptrı diye işaretledik.
                author = "Anounymous";
            }
            else
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;

                // ali => Ali
                AuthorName = textInfo.ToTitleCase(author.Trim());
            }

        }

        private void SetContent(string content)
        {
            if(string.IsNullOrEmpty(content))
            {
                throw new Exception("Makele içeriği boş geçilemez");
            }

            this.Content = content.Trim();
        }

        private void SetShortContent(string shortContent)
        {
            //if(shortContent.Length < 200)
            //{
            //    throw new Exception("Makale için en az 200 karakterlik bir yazı giriniz");
            //}

            this.ShortContent = shortContent.Trim();
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new Exception("Makale başlığını boş geçemezsiniz");
            }

            this.Title = title.Trim();
        }

        public void AddComment(Comment comment)
        {

            if (string.IsNullOrEmpty(comment.Text))
            {
                throw new Exception("Yorum işin mesajınızı giriniz");
            }

            if(string.IsNullOrEmpty(comment.CommentBy))
            {
                throw new Exception("Yorum yapmak için mail adresiniz veya kullanıcı adınızı giriniz");
            }

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            // ahmet  => Ahmet
            comment.CommentBy = textInfo.ToTitleCase(comment.CommentBy.Trim());
            comment.Text = comment.Text.Trim();

            _comments.Add(comment);
        }

        public void AddTag(Tag tag, ITagRepository tagRepository)
        {
            Tag existingTag = tagRepository.FindByName(tag.Name);

            // tag yoksa
            if (existingTag == null)
            {
                if (string.IsNullOrEmpty(tag.Name))
                {
                    throw new Exception("Makaleye Boş bir etiket ekleyemezsiniz");
                }

                // küçük harfe çevirip trimleyim önüne # koyduk
                // gs sampiyon => #gssampiyon
                tag.Name = tag.Name.Replace(" ", "");
                tag.Name = $"#{tag.Name.ToLower().Trim()}";
                _tags.Add(tag);
                // Tag Tag tablosunda olmadığı için Hem Tag tablosuna kayıt atar hemde ara tablo olan Tag Post Tablosuna Id kaydı atar.
            }
            else
            {
                // bu tag daha öncedden sistemde olduğu için TagPost ara tablosunda kayıt ekleme işlemi yapar.
                // Tag tablosuna bir kayıt atmaz
                _tags.Add(existingTag);
            }

           
        }
   

    }
}
