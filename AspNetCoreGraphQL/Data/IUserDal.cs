using AspNetCoreGraphQL.DTO.User;
using AspNetCoreGraphQL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.Data
{
    public interface IUserDal
    {
        IQueryable<User> All();
        Task<IEnumerable<User>> IncludeUserGroupsAsync();
        IQueryable<UserListResponseDTO> IncludeUserGroupsDTO();
        IQueryable<User> GetUsersWithByUserGroupId(Int64 GroupID);
        User GetId(int id);
        IQueryable<UserListResponseDTO> List(UserListRequestDTO src);
    }
}
