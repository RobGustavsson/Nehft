using System.Collections.Generic;

namespace Nehft.Server
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string Number { get; }
        public string Town { get; }
        public string Zipcode { get; }

        private Address(string street, string number, string town, string zipcode)
        {
            Street = street;
            Number = number;
            Town = town;
            Zipcode = zipcode;
        }

        public static Result<Address, string> Create(string street, string number, string town, string zipcode)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                return Result<Address, string>.Error("Street has to be specified");
            }

            if (string.IsNullOrWhiteSpace(number))
            {
                return Result<Address, string>.Error("Number has to be specified");
            }

            if (string.IsNullOrWhiteSpace(town))
            {
                return Result<Address, string>.Error("Town has to be specified");
            }

            if (string.IsNullOrWhiteSpace(zipcode))
            {
                return Result<Address, string>.Error("Zipcode has to be specified");
            }

            return Result<Address, string>.Success(new Address(street, number, town,zipcode));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return Town;
            yield return Zipcode;
        }
    }
}