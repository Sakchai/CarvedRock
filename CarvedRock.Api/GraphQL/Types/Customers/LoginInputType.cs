using CarvedRock.Api.Data.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarvedRock.Api.GraphQL.Types.Customers
{
    public class LoginInputType : InputObjectGraphType<User>
    {
        public LoginInputType()
        {
            Name = "LoginInput";
            Field(x => x.Username);
            Field(x => x.Password);
        }
    }
}