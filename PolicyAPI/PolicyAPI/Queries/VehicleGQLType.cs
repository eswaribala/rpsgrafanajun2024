using GraphQL.Types;
using PolicyAPI.Models;

namespace PolicyAPI.Queries
{
    public class VehicleGQLType:ObjectGraphType<Vehicle>
    {
        public VehicleGQLType()
        {
            Name = "Vehicle";
            Field(_ => _.RegistrationNo).Description("Registration No");
            Field(_ => _.EngineNo).Description("Engine No");
            Field(_ => _.ChassisNo).Description("Chassis No");
            Field(_ => _.DOR).Description("Date of Registration");
            Field(_ => _.Maker).Description("Make");
            Field(_ => _.Color).Description("Color");
            Field<StringGraphType>("fuelType",
               resolve: context =>
               context.Source.FuelType.ToString());
        }
    }
}
