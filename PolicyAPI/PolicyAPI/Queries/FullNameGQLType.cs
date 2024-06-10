using GraphQL.Types;
using PolicyAPI.Models;

namespace PolicyAPI.Queries
{
    public class FullNameGQLType:ObjectGraphType<FullName>
    {
        public FullNameGQLType()
        {
            Name = "FullName";
            Field(_ => _.FirstName).Description("First Name");
            Field(_ => _.LastName).Description("Last Name");
            Field(_ => _.MiddleName).Description("Middle Name");

        }
    }
}
