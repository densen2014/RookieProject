using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgvSample
{
    class ItemTest
    {
        public ItemTest(int ID, string Name, string Description, CtrlType CtrlType)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.CtrlType = CtrlType;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CtrlType CtrlType { get; set; }
    }
}
