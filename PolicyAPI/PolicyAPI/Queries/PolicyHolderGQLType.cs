using GraphQL.Types;
using PolicyAPI.Models;

namespace PolicyAPI.Queries
{
    public class PolicyHolderGQLType:ObjectGraphType<PolicyHolder>
    {
        public PolicyHolderGQLType()
        {
            Name = "PolicyHolders";
            Field(_ => _.AdharCardNo).Description("Adharcard No");
            Field(_ => _.Name.FirstName).Description("Adharcard No");
            Field(_ => _.Name.LastName).Description("Adharcard No");
            Field(_ => _.Name.MiddleName).Description("Adharcard No");
            Field(_ => _.DOB).Description("Adharcard No");
            Field(_ => _.Email).Description("Adharcard No");
            Field(_ => _.Phone).Description("Adharcard No");
            Field<StringGraphType>("gender",
                resolve: context =>
                context.Source.Gender.ToString());
            




        }
    }
}
