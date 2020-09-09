using AspNetCoreGraphQL.DTO.User;
using AspNetCoreGraphQL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.Business
{
    public interface IUserService
    {
        IList<User> All();
        Task<IEnumerable<User>> IncludeUserGroupsAsync();
        IEnumerable<UserListResponseDTO> IncludeUserGroupsDTO();
        User GetId(int id);
        IList<User> GetUsersWithByUserGroupId(Int64 GroupID);
        IList<UserListResponseDTO> List(UserListRequestDTO src);
    }
}
