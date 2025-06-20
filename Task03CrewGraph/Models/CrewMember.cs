using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task03CrewGraph.Models
{
    public class CrewMember
    {
        public string Name { get; set; }
        public List<CrewMember> Subordinates { get; set; } = new();

        public CrewMember Parent { get; set; }
    }

}
