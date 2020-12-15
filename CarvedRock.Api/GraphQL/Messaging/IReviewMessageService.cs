using CarvedRock.Api.Data.Entities;
using CarvedRock.Api.GraphQL.Messaging;
using System;

namespace CarvedRock.Api
{
    public interface IReviewMessageService
    {
        ReviewAddedMessage AddReviewAddedMessage(ProductReview review);
        IObservable<ReviewAddedMessage> GetMessages();
    }
}