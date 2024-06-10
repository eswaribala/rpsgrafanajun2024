using GraphQL;
using GraphQL.Types;
using PolicyAPI.Models;
using PolicyAPI.Queries;
using PolicyAPI.Repositories;

namespace PolicyAPI.Mutations
{
    public class RootMutation:ObjectGraphType
    {

        public RootMutation(IVehicleRepo vehicleRepo) {

            FieldAsync<VehicleGQLType>(
                Name = "AddVehicle",
                arguments: new QueryArguments(new QueryArgument<VehicleGQLInputType>
                {
                    Name = "vehicleInput"
                }),
                resolve: async context =>  await vehicleRepo.AddVehicle(
                       context.GetArgument<Vehicle>("vehicleInput"))               
                );


            FieldAsync<BooleanGraphType>(
                Name = "DeleteVehicle",
                 arguments: new QueryArguments(new QueryArgument<StringGraphType>
                 {
                     Name = "registrationNo"
                 }),
                resolve: async context => await vehicleRepo.DeleteVehicle
                (context.GetArgument<string>("registrationNo"))
                );
        
        
        }
    }
}
