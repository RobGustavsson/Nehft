using Nehft.Server.Horses;
using System;
namespace Nehft.Server.Customers.GetHorses
{
    public class GetCustomerHorsesResult
    {
        public GetCustomerHorsesResult(Guid id, string name, HorseType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public HorseType Type { get; set; }
    }
}
