using CarvedRock.Api.Data.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarvedRock.Api.GraphQL.Types.Customers
{
    internal class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            Name = nameof(User);
            Field(x => x.Id).Description("User Id");
            Field(x => x.Username).Description("User's Username");
        }
    }
}