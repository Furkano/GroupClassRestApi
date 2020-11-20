using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Infrastructure.Repositories
{
    public class ClassRepository:RepositoryBase<Class>,IClassRepository
    {
        public ClassRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
