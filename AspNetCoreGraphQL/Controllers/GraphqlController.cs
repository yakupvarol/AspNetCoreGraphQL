using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreGraphQL.GraphQL;
using GraphQL;
using GraphQL.Conversion;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreGraphQL.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly IValidationRule _validationRule;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GraphQLController(
            ISchema schema,
            IDocumentExecuter documentExecuter,
            IValidationRule validationRule,
            IHttpContextAccessor httpContextAccessor)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _validationRule = validationRule;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphqlQueryParameter query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var executionOptions = new ExecutionOptions()
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = query.Variables?.ToInputs(), //Jsondaki variable kısmının tanımlanması için
                FieldNameConverter = new PascalCaseFieldNameConverter(), //Graphql sorgularının pascal case olarak yazılması için
                ValidationRules = new List<IValidationRule> { _validationRule },
                UserContext = _httpContextAccessor.HttpContext.User
            };

            try
            {
                var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

                if (result.Errors?.Count > 0)
                {
                    return BadRequest(result);
                }

                return Ok(result.Data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}