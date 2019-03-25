using System;
using MediatR;

namespace Nehft.Server.Animals.AddHorse
{
    public class AddHorseCommand : IRequest
    {
        public Guid Customer { get; }
        public string Name { get; }
        public string Type { get; }

        public AddHorseCommand(Guid customer, string name, string type)
        {
            Customer = customer;
            Name = name;
            Type = type;
        }
    }
}