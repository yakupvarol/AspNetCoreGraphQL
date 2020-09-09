using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.GraphQL.Types;
using GraphQL.Types;

namespace AspNetCoreGraphQL.GraphQL
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(IUserService userService, IUserGroupService userGroupService)
        {
            Name = "User Query";

            Field<ListGraphType<UsersType>>("Usera", resolve: ctx => userService.All());


            Field<UserGroupType>(
                "UserGroup",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("Id");
                    return userGroupService.GetId(id);
                }
            );
        }            
    }
}
