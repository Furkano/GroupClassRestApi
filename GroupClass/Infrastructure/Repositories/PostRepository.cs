using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Infrastructure.Repositories
{
    public class PostRepository:RepositoryBase<Post>,IPostRepository
    {
        public PostRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }
    }
}
