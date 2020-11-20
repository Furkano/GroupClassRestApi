using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IClassRepository Class { get; }
        IMemberRepository Member { get; }
        IPostRepository Post { get; }
        Task<bool> SaveChangesAsync();
    }
}
