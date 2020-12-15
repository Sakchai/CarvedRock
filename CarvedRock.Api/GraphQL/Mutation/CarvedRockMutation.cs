using CarvedRock.Api.Data.Entities;
using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.GraphQL.Types.Customers;
using CarvedRock.Api.Repositories;
using GraphQL;
using GraphQL.Types;
using System;
using YesSql;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockMutation : ObjectGraphType
    {
        private Defer<IProductReviewRepository> _reviewRepository;
        private Defer<IReviewMessageService> _messageService;

        public CarvedRockMutation(Defer<IProductReviewRepository> reviewRepository, Defer<IReviewMessageService> messageService)
        {
            _reviewRepository = reviewRepository;
            _messageService = messageService;
            CreateProductReview();

            //Field<UserGraphType>(
            //  "userLogin",
            //  arguments: new QueryArguments(
            //    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "credentials" }
            //  ),
            //  resolve: context =>
            //  {
            //      using (var session = store.Value.CreateSession())
            //      {
            //          //session.Save(customer);

            //          var test = session.Query<Customer>().FirstOrDefaultAsync().Result;
            //      }
            //      return new User { Id = 1, Username = "Test" };
            //  });
        }

        private void CreateProductReview()
        {
            FieldAsync<ProductReviewType>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductReviewInputType>> { Name = "review" }),

                resolve: async context =>
                {
                    var review = context.GetArgument<ProductReview>("review");
                    await _reviewRepository.Value.AddReview(review);
                    _messageService.Value.AddReviewAddedMessage(review);
                    return review;
                });
        }

    }
}
