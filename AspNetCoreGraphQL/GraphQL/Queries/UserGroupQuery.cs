using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.Entities;
using AspNetCoreGraphQL.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using System.Collections.Generic;

namespace AspNetCoreGraphQL.GraphQL
{
    public class UserGroupQuery : ObjectGraphType
    {
        public UserGroupQuery(IUserGroupService userGroupService)
        {
            Name = "UserGroupQuery";

            Field<ListGraphType<UserGroupType>>("UserGroups", resolve: ctx => userGroupService.All());

            Field<UserGroupType>(
               "UserGroupID",
               arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
               { Name = "Id", Description = "UserGroup Id" }),
               resolve: context =>
               {
                   var id = context.GetArgument<int>("Id");
                   if (id == 0)
                   {
                       context.Errors.Add(new ExecutionError("aaaa must be greater than zero!"));
                       return new List<UserGroup>();
                   }
                   return userGroupService.GetId(id);
               }
           );

            /*
              FieldAsync<ListGraphType<GenericObjectGraphType<T>>>("query",
                arguments: new QueryArguments {
                new QueryArgument<IntGraphType> { Name = "skip", DefaultValue = 0 },
                new QueryArgument<IntGraphType> { Name = "take", DefaultValue = 10 },
                },
                resolve: async context =>
                {
                    var skip = context.GetArgument<int>("skip");
                    var take = context.GetArgument<int>("take");
                    return await repository.Get().Skip(skip).Take(take).ToListAsync();
                });
             * /

            /*
            FieldAsync<ListGraphType<AuthorType>>(
               name: "authors",
               resolve: async context => await authorService.GetAuthorsAsync()
           );

            var args = new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" });

            FieldAsync<AuthorType>(
                name: "authorById",
                arguments: args,
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await authorService.GetAuthorByIdAsync(id);
                });
*/
        }
    }
}
