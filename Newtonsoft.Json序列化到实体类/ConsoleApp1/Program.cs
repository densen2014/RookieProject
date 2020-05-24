using Newtonsoft.Json;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = @"{""BigIntSupported"":9958158950,""date"":""20180322"",""message"":""Success !"",""status"":200,""city"":""北京"",""count"":632,""data"":{""shidu"":""34%"",""pm25"":73,""pm10"":91,""quality"":""良"",""wendu"":""5"",""ganmao"":""极少数敏感人群应减少户外活动"",""yesterday"":{""date"":""21日星期三"",""sunrise"":""06:19"",""high"":""高温 11.0℃"",""low"":""低温 1.0℃"",""sunset"":""18:26"",""aqi"":85,""fx"":""南风"",""fl"":""<3级"",""type"":""多云"",""notice"":""阴晴之间，谨防紫外线侵扰""},""forecast"":[{""date"":""22日星期四"",""sunrise"":""06:17"",""high"":""高温 17.0℃"",""low"":""低温 1.0℃"",""sunset"":""18:27"",""aqi"":98,""fx"":""西南风"",""fl"":""<3级"",""type"":""晴"",""notice"":""愿你拥有比阳光明媚的心情""},{""date"":""23日星期五"",""sunrise"":""06:16"",""high"":""高温 18.0℃"",""low"":""低温 5.0℃"",""sunset"":""18:28"",""aqi"":118,""fx"":""无持续风向"",""fl"":""<3级"",""type"":""多云"",""notice"":""阴晴之间，谨防紫外线侵扰""},{""date"":""24日星期六"",""sunrise"":""06:14"",""high"":""高温 21.0℃"",""low"":""低温 7.0℃"",""sunset"":""18:29"",""aqi"":52,""fx"":""西南风"",""fl"":""<3级"",""type"":""晴"",""notice"":""愿你拥有比阳光明媚的心情""},{""date"":""25日星期日"",""sunrise"":""06:13"",""high"":""高温 22.0℃"",""low"":""低温 7.0℃"",""sunset"":""18:30"",""aqi"":71,""fx"":""西南风"",""fl"":""<3级"",""type"":""晴"",""notice"":""愿你拥有比阳光明媚的心情""},{""date"":""26日星期一"",""sunrise"":""06:11"",""high"":""高温 21.0℃"",""low"":""低温 8.0℃"",""sunset"":""18:31"",""aqi"":97,""fx"":""西南风"",""fl"":""<3级"",""type"":""多云"",""notice"":""阴晴之间，谨防紫外线侵扰""}]}}";
            var json1 = JsonConvert.DeserializeObject<Rootobject>(test);//反序列化

        }
    }


    public class Rootobject
    {
        public long BigIntSupported { get; set; }
        public string date { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public string city { get; set; }
        public int count { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string shidu { get; set; }
        public int pm25 { get; set; }
        public int pm10 { get; set; }
        public string quality { get; set; }
        public string wendu { get; set; }
        public string ganmao { get; set; }
        public Yesterday yesterday { get; set; }
        public Forecast[] forecast { get; set; }
    }

    public class Yesterday
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
