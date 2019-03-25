using System;
using MediatR;

namespace Nehft.Server.Horses.AddHorse
{
    public class AddHorseCommand : IRequest
    {
        public Guid Customer { get; }
        public string Name { get; }
        public HorseType Type { get; }
        public string Breed { get; }
        public int YearOfBirth { get; }
        public string Exterior { get; }
        public string History { get; }
        public Address Address { get; }

        private AddHorseCommand(Guid customer,
            string name,
            HorseType type,
            string breed,
            int yearOfBirth,
            string exterior,
            string history,
            Address address)
        {
            Customer = customer;
            Name = name;
            Type = type;
            Breed = breed;
            YearOfBirth = yearOfBirth;
            Exterior = exterior;
            History = history;
            Address = address;
        }

        public static Result<AddHorseCommand, string> Create(
            Guid customer,
            string name,
            string type,
            string breed,
            int yearOfBirth,
            string exterior,
            string history,
            string street,
            string houseNumber,
            string town,
            string zipCode)
        {
            if (!Enum.TryParse<HorseType>(type, true, out var horseType))
            {
                return Result<AddHorseCommand, string>.Error("Invalid horse type");
            }

            var addressOrError = Address.Create(street, houseNumber, town, zipCode);

            return addressOrError.OnSuccess(address =>
                new AddHorseCommand(customer, name, horseType, breed, yearOfBirth, exterior, history, address));

        }
    }
}