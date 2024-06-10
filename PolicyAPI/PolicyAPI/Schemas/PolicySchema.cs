using GraphQL.Types;
using PolicyAPI.Mutations;
using PolicyAPI.Queries;

namespace PolicyAPI.Schemas
{
    public class PolicySchema:Schema
    {
        public PolicySchema(IServiceProvider serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
            Mutation = serviceProvider.GetRequiredService<RootMutation>();

        }
    }
}
