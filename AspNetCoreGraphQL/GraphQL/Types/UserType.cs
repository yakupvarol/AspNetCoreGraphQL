using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL.Types
{
    public class UsersType : ObjectGraphType<User>
    {
        public UsersType(IUserGroupService userGroupService)
        {
            // "UserGroups" : Users route içinde, bulunan yeni rouute (end point) 
            //Field: GraphType’mızın hangi propertylere sahip olacağının belirlendiği kısımdır.
            
            Name = "User";
            Description = "Kullanıcıya Ait Alanlar ve İlişkili Tablolar";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the User.");
            Field(p => p.FirstName).Description("User First Name");
            Field(p => p.LastName).Description("User Last Name");
            Field(p => p.Isactive, nullable: true).DefaultValue(true).Description("User Active");

            //Example 1
            //FieldAsync<UserGroupType>("UserGroups", resolve: async context => await userGroupService.GetId(context.Source.Groupid));
            //Field<UserGroupType>("UserGroups", resolve: context => userGroupService.GetId(context.Source.Groupid));

            //Example 2
            Field<UserGroupType>("UserGroups", resolve: _ => _.Source.Group);

            //Field(p => p.Group, type: typeof(UserGroupType)).Description("User's Groups");
        }
    }
}
