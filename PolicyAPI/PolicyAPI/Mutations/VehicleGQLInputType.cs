using GraphQL.Types;
using PolicyAPI.Models;

namespace PolicyAPI.Mutations
{
    public class VehicleGQLInputType:InputObjectGraphType
    {

        public VehicleGQLInputType()
        {
            Name = "VehicleInput";
            Field<NonNullGraphType<StringGraphType>>("RegistrationNo");
            Field<NonNullGraphType<StringGraphType>>("Maker");
            Field<NonNullGraphType<StringGraphType>>("Color");
            Field<NonNullGraphType<StringGraphType>>("EngineNo");
            Field<NonNullGraphType<StringGraphType>>("ChassisNo");
            Field<NonNullGraphType<StringGraphType>>("DOR");
            Field<NonNullGraphType<EnumerationGraphType<FuelType>>>("FuelType");
        }
    }
}
