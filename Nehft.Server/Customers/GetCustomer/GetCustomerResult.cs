﻿namespace Nehft.Server.Customers.GetCustomer
{
    public class GetCustomerResult
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        public GetCustomerResult(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}