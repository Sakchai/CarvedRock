using CarvedRock.Api.GraphQL.Messaging;
using CarvedRock.Api.GraphQL.Types;
using GraphQL.Resolvers;
using GraphQL.Types;
using System;
namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockSubscription: ObjectGraphType
    {
        public CarvedRockSubscription(Defer<ReviewMessageService> messageService)
        {
            Name = "Subscription";
            AddField(new EventStreamFieldType
            {
                Name = "reviewAdded",
                Type = typeof(ReviewAddedMessageType),
                Resolver = new FuncFieldResolver<ReviewAddedMessage>(c => c.Source as ReviewAddedMessage),
                Subscriber = new EventStreamResolver<ReviewAddedMessage>(c => messageService.Value.GetMessages())
            });
        }
    }
}
