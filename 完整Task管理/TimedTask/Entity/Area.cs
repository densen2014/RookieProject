using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimedTask.Entity
{
    public class Area
    {
        public int ID { get; set; }
        public int ZoneID { get; set; }
        public string AreaCode { get; set; }
        public string Name { get; set; }
    }

    public class Province
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
