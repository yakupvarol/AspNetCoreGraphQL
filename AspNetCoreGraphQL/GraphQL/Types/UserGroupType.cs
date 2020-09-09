using AspNetCoreGraphQL.Business;
using AspNetCoreGraphQL.Entities;
using GraphQL.Types;

namespace AspNetCoreGraphQL.GraphQL.Types
{
    public class UserGroupType : ObjectGraphType<UserGroup>
    {
        public UserGroupType(IUserService userService)
        {
            Name = "UserGroup";
            Description = "a person";

            //Field(_ => _.Id);
            //Field<int>(p => p.Id);
            //Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Book.");
            Field(p => p.Id);
            Field(p => p.Name);
            //Field(x => x.Name).Description("The name of the Author");
            //Field<string>(p => p.Name);
            Field(p => p.Isactive, nullable:true).DefaultValue(true);
            //Field(p => p.Name, true);
            //Field(x => p.Name, nullable: true).Description("Song description.");
            //Field<StringGraphType>("birthDate", resolve: context => context.Source.BirthDate.ToShortDateString());
            //Field<ProductStatusEnumType>(nameof(Product.Status), "The status of the product");
            Field<ListGraphType<UsersType>>("Users", resolve: _ => _.Source.Users);
            //Field(p => p.Users, type: typeof(ListGraphType<UsersType>)).Description("Author's books");
            /*
            Field<ListGraphType<UsersType>>(
                "Users",
                resolve: context => userService.GetUsersWithByUserGroupId(context.Source.Id)
            );
            */

            /*
            FieldAsync<ListGraphType<OrderType>>("orders",
            resolve: async context => await dbContext.Orders.Where(order => order.UserId == context.Source.Id).ToListAsync());
            */

            /*
            Field<ListGraphType<SkaterStatisticType>>("skaterSeasonStats",
               arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
               resolve: context => contextServiceLocator.SkaterStatisticRepository.Get(context.Source.Id), description: "Player's skater stats");
            */
        }
    }

    /*
     public class ProductStatusEnumType : EnumerationGraphType<ProductStatus>
    {
        public ProductStatusEnumType()
        {
            Name = nameof(Models.Product.Status);
            Description = "The status of the product";
        }
    }
     */
        }


