using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }

        public Product()
        {
        }
        public Product(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
