using System.Collections.Generic;

namespace Restaurant.API.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public List<Menu> Menus { get; set; }
        public string Name { get; set; }
    }
}