namespace Nehft.Server.Customers
{
    public class Animal
    {
        public string Name { get; }
        public string Type { get; }

        public Animal(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}