using GraphQL.Types;
using PolicyAPI.Models;

namespace PolicyAPI.Queries
{
    public class AddressGQLType:ObjectGraphType<Address>
    {

        public AddressGQLType(){
            Name = "Address";
            Field(_ => _.AddressId).Description("Address Id");
            Field(_ => _.DoorNo).Description("Door No");
            Field(_ => _.StreetName).Description("Street Name");
            Field(_ => _.City).Description("City");
            Field(_ => _.State).Description("State");
            Field(_ => _.Country).Description("Country");
            Field(_ => _.PinCode).Description("Pincode");
            Field(_ => _.AdharCardNo).Description("AdharCard No");

        }
    }
}
