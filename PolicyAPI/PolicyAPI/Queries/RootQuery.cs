

using GraphQL;
using GraphQL.Types;
using PolicyAPI.Repositories;

namespace PolicyAPI.Queries
{
    public class RootQuery:ObjectGraphType
    {

        public RootQuery(IVehicleRepo vehicleRepo,
            IPolicyHolderRepo policyHolderRepo,
            IPolicyRepo policyRepo,
            IAddressRepo addressRepo
            ) {

            
            //all vehicles
            Field<ListGraphType<VehicleGQLType>>(
            "vehicles",
                    resolve: context => vehicleRepo.GetAllVehicles());



            //get vehicle by id
            Field<VehicleGQLType>(
               "vehicle",
               arguments: new QueryArguments(new
               QueryArgument<StringGraphType>
               { Name = "registrationNo" }),
               resolve: context => vehicleRepo
               .GetVehicleById(context.GetArgument<string>("registrationNo"))

               );


            Field<ListGraphType<PolicyHolderGQLType>>(
                Name="policyholders",
                resolve: context=>policyHolderRepo.GetAllPolicyHolders()
                
                );

            Field<PolicyHolderGQLType>(
                Name = "policyholder",
                arguments: new QueryArguments(new
                QueryArgument<StringGraphType>
                {
                    Name = "adharCardNo"
                }),
                resolve: context => policyHolderRepo
                .GetPolicyHolderById(context.GetArgument<string>("adharCardNo")

                ));


            Field<ListGraphType<PolicyGQLType>>(
                Name="policies",
                resolve: context=>policyRepo.GetAllPolicies()
                );

            Field<PolicyGQLType>(
                Name = "policy",
                arguments: new QueryArguments(new QueryArgument<LongGraphType>
                {
                    Name = "policyNo"
                }),
                resolve: context => policyRepo.GetPolicyByPolicyNo(context
                .GetArgument<long>("policyNo"))

                ) ;

            Field<ListGraphType<AddressGQLType>>(
                 Name = "addresses",
                  resolve: context => addressRepo.GetAllAddresss()
                ) ;

            Field<AddressGQLType>(
                Name = "address",
                arguments: new QueryArguments(new QueryArgument<StringGraphType>
                {
                    Name = "doorNo"
                },
                new QueryArgument<StringGraphType>
                {
                    Name = "streetName"
                },
                new QueryArgument<StringGraphType>
                {
                    Name = "adharCardNo"
                }
                ),
                resolve: context => addressRepo.GetAddressById(context.
                GetArgument<string>("doorNo"),
                context.
                GetArgument<string>("streetName"),
                context.
                GetArgument<string>("adharCardNo")


                )

                );



        }

    }
}
