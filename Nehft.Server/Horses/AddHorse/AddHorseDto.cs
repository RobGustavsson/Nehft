using System;

namespace Nehft.Server.Horses.AddHorse
{
    public class AddHorseDto
    {
        public Guid Customer { get; }
        public string Name { get; }
        public string Type { get; }
        public string Breed { get; }
        public int YearOfBirth { get; }
        public string Exterior { get; }
        public string History { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string Town { get; }
        public string ZipCode { get; }

        public AddHorseDto(
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
            Customer = customer;
            Name = name;
            Type = type;
            Breed = breed;
            YearOfBirth = yearOfBirth;
            Exterior = exterior;
            History = history;
            Street = street;
            HouseNumber = houseNumber;
            Town = town;
            ZipCode = zipCode;
        }
    }
}