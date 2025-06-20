using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02StarWarsApi.Models
{
    public class Planet
    {
        public string name { get; set; }
        public List<string> residents { get; set; }
        public string url { get; set; }
    }
}
