using AspNetCoreGraphQL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.Data
{
    public interface IUserGroupDal
    {
        IQueryable<UserGroup> All();
        UserGroup GetId(long id);
    }
}
