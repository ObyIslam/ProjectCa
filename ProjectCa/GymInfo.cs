using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCa
{
    public class GymInfo
    {
        public List<GymTopic> fitness_info = new List<GymTopic>();
    }

    public class GymTopic
    {
        public string Topic { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }
}
