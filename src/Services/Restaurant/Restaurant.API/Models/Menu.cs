using System.Collections.Generic;

namespace Restaurant.API.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public string Name { get; set; }
    }
}