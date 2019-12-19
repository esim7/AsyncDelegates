namespace Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Category()
        {
        }
        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}