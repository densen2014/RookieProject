using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsJsonTreeView
{
    [AddINotifyPropertyChangedInterface]
    public class Rootobject
    {
        public string date { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public string city { get; set; }
        public int count { get; set; }
      
       [OnChangedMethod(nameof(OnMyNameChanged))]
       public Data data { get; set; }

        private void OnMyNameChanged()
        {
            Debug.WriteLine("data changed");
        }

        [AddINotifyPropertyChangedInterface]
        public class Data
        {
            public string shidu { get; set; }
            public int pm25 { get; set; }
            public int pm10 { get; set; }
            public string quality { get; set; }
            public string wendu { get; set; }
            public string ganmao { get; set; }
            public Yesterday yesterday { get; set; }
            public List<Forecast> forecast { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class Yesterday
        {
            [OnChangedMethod(nameof(OnMyNameChanged))]
            public string date { get; set; }
            public string sunrise { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string sunset { get; set; }
            public int aqi { get; set; }
            public string fx { get; set; }
            public string fl { get; set; }
            public string type { get; set; }
            public string notice { get; set; }
            private void OnMyNameChanged()
            {
                Debug.WriteLine("data changed");
            }
        }

        [AddINotifyPropertyChangedInterface]
        public class Forecast
        {
            public string date { get; set; }
            public string sunrise { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string sunset { get; set; }
            public int aqi { get; set; }
            public string fx { get; set; }
            public string fl { get; set; }
            public string type { get; set; }
            public string notice { get; set; }
        }
    }

}
