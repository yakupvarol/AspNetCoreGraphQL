using AspNetCoreGraphQL.Data;
using AspNetCoreGraphQL.DTO.User;
using AspNetCoreGraphQL.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.Business
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private readonly ILogger<IUserDal> _log;

        public UserManager(IUserDal userDal, ILogger<IUserDal> log)
        {
            _userDal = userDal;
            _log = log;
        }

        public IList<UserListResponseDTO> List(UserListRequestDTO src)
        {
            return _userDal.List(src).ToList();
        }

        public IList<User> All()
        {
            return _userDal.All().ToList();
        }

        public IList<User> GetUsersWithByUserGroupId(long GroupID)
        {
            return _userDal.GetUsersWithByUserGroupId(GroupID).ToList();
        }

        public async Task<IEnumerable<User>> IncludeUserGroupsAsync()
        {
            return await _userDal.IncludeUserGroupsAsync();
        }

        public IEnumerable<UserListResponseDTO> IncludeUserGroupsDTO()
        {
            return _userDal.IncludeUserGroupsDTO().ToList();
        }

        public User GetId(int id)
        {
            return _userDal.GetId(id);
        }
    }
}
