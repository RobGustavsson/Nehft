namespace Nehft.Server.Customers
{
    public class Name
    {
        public string FirstName { get; }
        public string LastName { get; }

        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<Name, string> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result<Name, string>.Error("First name has to be specified");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result<Name, string>.Error("Last name has to be specified");
            }

            return Result<Name, string>.Success(new Name(firstName, lastName));
        }
    }
}