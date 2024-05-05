using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCa
{
    public class Excercises
    {
        public List<ExcerciseInfo> exercises = new List<ExcerciseInfo>();
    }

    public class ExcerciseInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string video { get; set; }

    }


}
