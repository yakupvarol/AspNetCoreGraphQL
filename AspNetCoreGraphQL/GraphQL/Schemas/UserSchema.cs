using AspNetCoreGraphQL.GraphQL.Queries;
using GraphQL;
using GraphQL.Types;

namespace AspNetCoreGraphQL.GraphQL
{
    public class UserSchema : Schema
    {
        public UserSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            //Query = resolver.Resolve<UserQuery>();
            //Query = resolver.Resolve<UserGroupQuery>();
        }
    }
}
