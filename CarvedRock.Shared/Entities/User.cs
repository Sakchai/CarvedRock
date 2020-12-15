using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.Api.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Password { get; set; }
    }
}