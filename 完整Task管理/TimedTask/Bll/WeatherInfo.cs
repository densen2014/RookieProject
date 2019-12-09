using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimedTask.Bll
{
    /// <summary>
    /// 天气信息实体类
    /// </summary>
    public class WeatherInfo
    {
        public WeatherInfo(string[] data)
        {
            if (data == null || data.Length < 10)
                return;

            this.ProvinceName = data[0];
            this.CityName = data[1];
            this.CityCode = data[2];
            this.CityImage = data[3];
            this.RefreshTime = Convert.ToDateTime(data[4]);
            this.TodayTemperature = data[5];
            this.TodaySurvey = data[6];
            this.TodayWind = data[7];
            this.TodayStartImage = GetDisplayImageUri(data[8]);
            this.TodayEndImage = GetDisplayImageUri(data[9]);
            this.TodayWeatherContent = data[10];
            this.TodayWeatherSummary = data[11];
            this.TomorrowTemperature = data[12];
            this.TomorrowSurvey = data[13];
            this.TomorrowWind = data[14];
            this.TomorrowStartImage = GetDisplayImageUri(data[15]);
            this.TomorrowEndImage = GetDisplayImageUri(data[16]);
            this.HtTemperature = data[17];
            this.HtSurvey = data[18];
            this.HtWind = data[19];
            this.HtStartImage = GetDisplayImageUri(data[20]);
            this.HtEndImage = GetDisplayImageUri(data[21]);
        }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 城市图片
        /// </summary>
        public string CityImage { get; set; }
        /// <summary>
        /// 最后刷新时间
        /// 这是服务器的最后刷新时间
        /// </summary>
        public DateTime RefreshTime { get; set; }
        /// <summary>
        /// 今日的气温
        /// </summary>
        public string TodayTemperature { get; set; }
        /// <summary>
        /// 今日天气概况
        /// </summary>
        public string TodaySurvey { get; set; }
        /// <summary>
        /// 今日凤向和风力
        /// </summary>
        public string TodayWind { get; set; }
        /// <summary>
        /// 今日天气趋势起始图片
        /// </summary>
        public string TodayStartImage { get; set; }
        /// <summary>
        /// 今日天气趋势结束图片
        /// </summary>
        public string TodayEndImage { get; set; }
        /// <summary>
        /// 今日天气实况
        /// </summary>
        public string TodayWeatherContent { get; set; }
        /// <summary>
        /// 今日天气和生活指数
        /// </summary>
        public string TodayWeatherSummary { get; set; }
        /// <summary>
        /// 明天的气温
        /// </summary>
        public string TomorrowTemperature { get; set; }
        /// <summary>
        /// 明天天气概况
        /// </summary>
        public string TomorrowSurvey { get; set; }
        /// <summary>
        /// 明天凤向和风力
        /// </summary>
        public string TomorrowWind { get; set; }
        /// <summary>
        /// 明天天气趋势起始图片
        /// </summary>
        public string TomorrowStartImage { get; set; }
        /// <summary>
        /// 明天天气趋势结束图片
        /// </summary>
        public string TomorrowEndImage { get; set; }
        /// <summary>
        /// 后天的气温
        /// </summary>
        public string HtTemperature { get; set; }
        /// <summary>
        /// 后天天气概况
        /// </summary>
        public string HtSurvey { get; set; }
        /// <summary>
        /// 后天凤向和风力
        /// </summary>
        public string HtWind { get; set; }
        /// <summary>
        /// 后天天气趋势起始图片
        /// </summary>
        public string HtStartImage { get; set; }
        /// <summary>
        /// 后天天气趋势结束图片
        /// </summary>
        public string HtEndImage { get; set; }

        /// <summary>
        /// 得到用于显示的图片路径
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetDisplayImageUri(string input)
        {
            var result = string.Format(Entity.App.StartPath.Replace("\\", "/") + "/Weather/a_{0}", input);
            return result;
        }
    }
}
