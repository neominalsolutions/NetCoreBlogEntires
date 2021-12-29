using NetCoreBlogEntires.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Services
{
    public interface IPostService
    {
        /// <summary>
        /// Post Input Model isteğinde bu methoda bulunulduğunda ilgili database işlemleri ve logişc bu method içerisinde çalışacak.
        /// </summary>
        /// <param name="request"></param>
        void Save(PostInputModel request);

    }
}
