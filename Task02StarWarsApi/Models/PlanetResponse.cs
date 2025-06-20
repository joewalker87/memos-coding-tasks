using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02StarWarsApi.Models
{
    public class PlanetResponse
    {
        public string next { get; set; }
        public List<Planet> results { get; set; }
    }
}
