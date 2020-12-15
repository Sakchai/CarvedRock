using CarvedRock.Api.Data;
using CarvedRock.Api.Data.Entities;
using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.GraphQL.Types.Customers;
using CarvedRock.Api.Repositories;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using YesSql;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockQuery : ObjectGraphType
    {
        private Defer<IProductRepository> _productRepository;
        private Defer<ICustomerRepository> _customerRepository;
        private Defer<IProductReviewRepository> _reviewRepository;
        public CarvedRockQuery(Defer<ICustomerRepository> customerRepository,
                               Defer<IProductRepository> productRepository, 
                               Defer<IProductReviewRepository> reviewRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _reviewRepository = reviewRepository;

            ProductGetAll();

            ProductById();

            ProductReviewByProductId();

            CustomerGetAll();

            CustomerById();
        }

        private void CustomerById()
        {
            Field<CustomerGraphType>("customer", "Returns a Single Customer",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return _customerRepository.Value.GetOne(id);
                });
        }

        private void CustomerGetAll()
        {
            Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer",
                resolve: context =>
                {
                    return _customerRepository.Value.GetAll();
                });
        }

        private void ProductReviewByProductId()
        {
            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "productId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("productId");
                    return _reviewRepository.Value.GetForProduct(id);
                });
        }

        private void ProductById()
        {
            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return _productRepository.Value.GetOne(id);
                }
            );
        }

        private void ProductGetAll()
        {
            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => _productRepository.Value.GetAll()
            );
        }

    }
}
