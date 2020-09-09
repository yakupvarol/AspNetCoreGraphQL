using AspNetCoreGraphQL.Entities;
using System.Collections.Generic;

namespace AspNetCoreGraphQL.Business
{
    public interface IUserGroupService
    {
        IList<UserGroup> All();
        UserGroup GetId(long id);
    }
}
