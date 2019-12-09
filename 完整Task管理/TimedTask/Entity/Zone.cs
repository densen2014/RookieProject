using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TimedTask.Entity
{
    public class Zone
    {
        public int ID { get; set; }
        public string Name { get; set; }        
        public ObservableCollection<Area> AreaList { get; set; }
        public Zone()
        {
            this.AreaList = new ObservableCollection<Area>();
        }
    }
}
