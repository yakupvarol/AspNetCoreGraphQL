using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL
{
    public class GraphqlQueryParameter
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }

    /*
        QUERY
         {
          UserGroup(Id: $Id) {
               Id,
            Name
          }
        }

        GRAPHQL VARIABLES
        {
	        "Id":1
        }

    */

    /*
     	  {
          UserGroup {
            Id,
            Name,
            Users {
            Id,
            FirstName,
            LastName
          }
          }
        }
     */

    /*
     {
          Users{
               Id,
            FirstName,
            LastName,
            UserGroup {
               Id,
            Name
          }
          }
        }
    */
}
