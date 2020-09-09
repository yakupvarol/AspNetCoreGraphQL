using AspNetCoreGraphQL.Core.DataAccess.EntityFramework.Repository;
using AspNetCoreGraphQL.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspNetCoreGraphQL.Data
{
    public class EfUserGroupDal : EfEntityRepositoryBase<UserGroup, EFContext>, IUserGroupDal
    {
        private readonly IMapper _mapper;
        private readonly EFContext _context;

        public EfUserGroupDal(IMapper mapper, EFContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IQueryable<UserGroup> All()
        {
            //return GetAll();
            return GetAll().Include("Users");

        }

        public UserGroup GetId(long id)
        {
            /*
            retun _context
                .UserGroups
                .Include(x => x.Users)
                .Include("Products").FirstOrDefaultAsync()
            */
            //return FindFirst(x => x.Id == id);
            return _context.UserGroups.Include("Users").Where(x => x.Id == id).FirstOrDefault();
        }

    }
}
