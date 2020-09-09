using AspNetCoreGraphQL.Data;
using AspNetCoreGraphQL.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreGraphQL.Business
{
    public class UserGroupManager : IUserGroupService
    {
        private IUserGroupDal _userGroupDal;
        private readonly ILogger<IUserGroupDal> _log;

        public UserGroupManager(IUserGroupDal userGroupDal, ILogger<IUserGroupDal> log)
        {
            _userGroupDal = userGroupDal;
            _log = log;
        }

        public IList<UserGroup> All()
        {
            return _userGroupDal.All().ToList();
        }

        public UserGroup GetId(long id)
        {
            return _userGroupDal.GetId(id);
        }
    }
}
