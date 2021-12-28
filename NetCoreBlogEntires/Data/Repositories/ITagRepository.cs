using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Repositories
{
    public interface ITagRepository: IRepository<Tag>
    {
        Tag FindByName(string tagName);
    }
}
