namespace Nehft.Server.Customers
{
    public class EmailAddress
    {
        public string Email { get; }

        private EmailAddress(string email)
        {
            Email = email;
        }

        public static Result<EmailAddress, string> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<EmailAddress, string>.Error("Email has to be specified");
            }

            return Result<EmailAddress, string>.Success(new EmailAddress(email));
        }
    }
}