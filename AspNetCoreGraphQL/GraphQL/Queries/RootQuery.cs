using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.GraphQL.Types;
using AspNetCoreGraphQL.Helper.Extens;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL.Queries
{

    public class RootQuery : ObjectGraphType
    {
        // https://graphql-dotnet.github.io/docs/getting-started/user-context/
        // Mevcut modellerimin GraphqlType lı hali: Querymizden dönen modellerimizin hangi propertyleri alacağını belirlediğimiz kısım.
        /*
            Sorgulama işlemlerinde geri dönüş tipleri değişkenlik göstermektedir.
            StringGraphType
            IntGraphType
            FloatGraphType
            BooleanGraphType
            IdGraphType
            DateGraphType
            ListGraphType
        */

        public RootQuery(IUserService userService, IUserGroupService userGroupService)
        {
            //Query bölümünü, Graphql endpointlerimizin belirlendiği yerdir. Graphql Route gibi düşünebilirsiniz.
            Name = "Root Query";
            Description = "Tüm Schema Ait, Query ve Route";

            //Graphql Route (EndPoint)
            UsersEndPoint(userService); // "Users" Graphql Route (endpoint)
            UserGroupsEndPoint(userGroupService); // "UserGroups" Graphql Route (endpoint) **//
            UserEndPoint(userService);
            UserMoreArgumentsEndPoint(userService);
        }

        private void UsersEndPoint(IUserService userService)
        {
            //*** "Users" Graphql Route (endpoint) **//
            // *** Tüm Kullanıcılar - (Not : Kullanıcının  ilişkili oldugu, tüm bölümler (alanlar, ilişki tablolar) UsersType Tanımlanıyor.) ***//

            // 1
            //query  { Users  { Id, FirstName, LastName, Isactive, UserGroups { Id, Name } } } -- Graphql Query
            //Field<ListGraphType<UsersType>>("Users", resolve: ctx => userService.All());

            // 2
            //query {Users { Id, FullName, Email, Isactive , UserGroups { Id,Name }}}
            //Field<ListGraphType<UserDTOType>>("Users", resolve: ctx => userService.IncludeUserGroupsDTO());

            // 3
            //query  { Users  { Id, FirstName, LastName, Isactive, UserGroups { Id, Name } } } -- Graphql Query
            FieldAsync<ListGraphType<UsersType>>("Users", resolve: async ctx => await userService.IncludeUserGroupsAsync()).AddPermissions("user"); ;
        }

        private void UserGroupsEndPoint(IUserGroupService userGroupService)
        {
            //*** "UserGroups" Graphql Route (endpoint) **//
            // Tüm Kullanıcı Grupları - (Not : Kullanıcı gruplarının,  ilişkili oldugu, tüm bölümler (alanlar, ilişki tablolar) UsersType Tanımlanıyor.)

            //query { UserGroups { Id, Name } } -- Graphql Query
            Field<ListGraphType<UserGroupType>>("UserGroups", resolve: ctx => userGroupService.All()).AddPermissions("user"); ;
        }

        private void UserEndPoint(IUserService userService)
        {
            //** Arguments: Graphql isteğinden alınacak parametreler varsa bu kısımda tanımlanır.
            //** Resolve: Graphql’in döneceği data bu kısımda ayarlanır.
            //** NonNullGraphType: Alanının "null" olmayacağını "IntGraphType" ile sonucun sayısal bir değer olacağını belirttik.

            //query { User(Id: 1) { Id, FirstName, LastName, Isactive, UserGroups { Id, Name } } } -- Graphql Query
            Field<UsersType>(
                "User",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "Id", Description="User ID" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("Id");
                    return userService.GetId(id);
                }
            );
        }

        private void UserMoreArgumentsEndPoint(IUserService userService)
        {
            //query { User(Id: 1) { Id, FirstName, LastName, Isactive, UserGroups { Id, Name } } } -- Graphql Query
            Field<UsersType>(
                "UserError",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>() { Name = "Id" },
                    new QueryArgument<StringGraphType>() { Name = "Name" }
                    ),
                resolve: context =>
                {

                    var id = context.GetArgument<int>("Id");
                    var Name = context.GetArgument<string>("Name");

                    // Throw a random exception.
                    if (id == 0)
                        throw new Exception("Random Exception");

                    // Check skip value and add ValidationError.
                    //if (context.HasArgument("skip") && context.GetArgument<int>("skip") < 0)
                    if (id==1)
                    {
                        context.Errors.Add(new ValidationError(context.Document.OriginalQuery, "skip", "The argument 'skip' can not be less than 0"));
                        return null;
                    }


                    //context.Errors.Add(new ExecutionError(errorMessage, ex));

                    return userService.GetId(id);
                }
            );
        }
    }
}
