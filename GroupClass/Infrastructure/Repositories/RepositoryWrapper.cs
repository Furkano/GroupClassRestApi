using GroupClass.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Infrastructure.Repositories
{
    public class RepositoryWrapper:IRepositoryWrapper
    {
        private AppDbContext appDbContext;
        private IUserRepository userRepository;
        private IPostRepository postRepository;
        private IMemberRepository memberRepository;
        private IClassRepository classRepository;
        public RepositoryWrapper( AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public IUserRepository User
        {
            get
            {
                if(userRepository==null)
                {
                    userRepository = new UserRepository(appDbContext);
                }
                return userRepository;
            }
            
        }
        public IClassRepository Class
        {
            get
            {
                if (classRepository==null)
                {
                    classRepository = new ClassRepository(appDbContext);
                }
                return classRepository;
            }
        }
        public IPostRepository Post
        {
            get
            {
                if (postRepository==null)
                {
                    postRepository = new PostRepository(appDbContext);
                }
                return postRepository;
            }
        }
        public IMemberRepository Member
        {
            get
            {
                if (memberRepository==null)
                {
                    memberRepository = new MemberRepository(appDbContext);
                }
                return memberRepository;
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await appDbContext.SaveChangesAsync()) > 0 ;
        }
    }
}
