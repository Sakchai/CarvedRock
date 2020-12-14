using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockSchema: Schema
    {

        public CarvedRockSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<CarvedRockQuery>();
            Mutation = provider.GetRequiredService<CarvedRockMutation>();
            Subscription = provider.GetRequiredService<CarvedRockSubscription>();
        }
    }
}
