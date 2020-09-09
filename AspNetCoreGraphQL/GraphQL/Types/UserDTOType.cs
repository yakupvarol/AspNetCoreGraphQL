using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.DTO.User;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL.Types
{
    public class UserDTOType : ObjectGraphType<UserListResponseDTO>
    {
        public UserDTOType(IUserGroupService userGroupService)
        {
            Name = "UserDTO";
            Description = "Kullanıcıya Ait Alanlar ve İlişkili Tablolar";

            Field(x => x.Id);
            Field(p => p.FullName).Description("User Full Name");
            Field(p => p.Email).Description("User Last Name");
            Field(p => p.Isactive, nullable: true).DefaultValue(true).Description("User Active");
            Field<UserGroupType>("UserGroups", resolve: context => userGroupService.GetId(context.Source.Groupid));
        }
    }
}
