using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02StarWarsApi.Models
{
    public class People
    {
        public string name { get; set; }
        public List<string> starships { get; set; }
    }
}
