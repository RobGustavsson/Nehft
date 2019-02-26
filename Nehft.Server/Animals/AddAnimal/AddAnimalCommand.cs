using System;
using MediatR;

namespace Nehft.Server.Animals.AddAnimal
{
    public class AddAnimalCommand : IRequest
    {
        public Guid Customer { get; }
        public string Name { get; }
        public string Type { get; }

        public AddAnimalCommand(Guid customer, string name, string type)
        {
            Customer = customer;
            Name = name;
            Type = type;
        }
    }
}