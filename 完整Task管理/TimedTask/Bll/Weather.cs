using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using TimedTask.WeatherService;

namespace TimedTask.Bll
{
    public sealed class Weather
    {
        private readonly string _filePath = System.Environment.CurrentDirectory + @"\Weather\Area.txt";
        private readonly string _weatherPath = System.Environment.CurrentDirectory + @"\Weather\{0}Weather.txt";//生成天气信息文件，每天只获取一次
        private WeatherWebServiceSoapClient weatherClient;   //获取气象信息的WebService对象

        /// <summary>
        /// 获得当前保存地区
        /// </summary>
        /// <returns></returns>
        public Entity.Area GetCurrentArea()
        {
            var file = new FileInfo(_filePath);
            Entity.Area result;
            if (!file.Exists)
            {
                //文件不存在就返回一个默认值,默认是成都
                result = new Entity.Area();
                result.ID = 250;
                result.Name = "成都";
                result.ZoneID = 23;
                //area.Zone = new Models.Zone() { ID = 23, Name = "四川" };
                result.AreaCode = "56294";
            }
            else
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Entity.Area));
                using (var stream = file.Open(FileMode.Open, FileAccess.Read))
                {
                    result = (Entity.Area)ser.ReadObject(stream);
                }
            }
            return result;
        }
        public void SaveCurrentArea(Entity.Area area)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Entity.Area));
            var file = new FileInfo(_filePath);
            FileStream stream;
            if (file.Exists)
            {
                stream = file.Open(FileMode.Truncate, FileAccess.Write);
            }
            else
            {
                stream = file.Open(FileMode.Create, FileAccess.Write);
            }
            using (stream)
            {
                ser.WriteObject(stream, area);
            }
        }

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="cityName">城市名或城市编号</param>
        /// <returns></returns>
        public string[] GetWeather(string cityName)
        {
            string[] weatherInfoList = null;
            string tmp = String.Empty;
            string path = String.Format(_weatherPath, cityName);
            if (String.IsNullOrEmpty(cityName))
                return null;

            //删除2天前文件
            Helper.DropFiles(Entity.App.StartPath + "\\Weather\\", cityName, new string[] { ".txt" }, 2);

            if (File.Exists(path))
            {
                FileInfo f = new FileInfo(path);
                if (f.LastWriteTime < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00")))
                    File.Delete(path);//删除昨天前文件

                tmp = Helper.ReadFile(path);
                weatherInfoList = tmp.Split(Entity.App.SpiderChar);
            }
            if (weatherInfoList != null && weatherInfoList.Length > 0)
                return weatherInfoList;

            #region 重新获取
            if (weatherClient == null)
                weatherClient = new WeatherWebServiceSoapClient("WeatherWebServiceSoap"); //实例化服务调用
            try
            {
                weatherInfoList = weatherClient.getWeatherbyCityName(cityName);
            }
            catch (System.Net.WebException webException)
            {

                throw webException;
            }
            catch (System.Net.Sockets.SocketException socketException)
            {
                throw socketException;
            }
            catch (System.NullReferenceException nullException)
            {
                throw nullException;
            }
            catch (System.Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (weatherClient != null)
                    weatherClient = null;
            }
            #endregion

            #region content
            //<string>直辖市</string>
            //<string>上海</string> 
            //<string>58367</string>
            //<string>58367.jpg</string> 
            //<string>2012-8-10 23:58:13</string>
            //<string>27℃/33℃</string> 
            //<string>8月11日 阵雨转多云</string>
            //<string>东南风4-5级</string> 
            //<string>3.gif</string>
            //<string>1.gif</string>
            //<string>今日天气实况：气温：28℃；风向/风力：北风 1级；湿度：80%；空气质量：良；紫外线强度：中等</string> 
            //<string>穿衣指数：天气炎热，建议着短衫、短裙、短裤、薄型T恤衫、敞领短袖棉衫等清凉夏季服装。 感冒指数：暂无。 运动指数：有降水，风力较强，较适宜在户内开展低强度运动，若坚持户外运动，请选择避雨防风地点。 洗车指数：不宜洗车，未来24小时内有雨，如果在此期间洗车，雨水和路上的泥水可能会再次弄脏您的爱车。 晾晒指数：有降水，可能会淋湿晾晒的衣物，不太适宜晾晒。请随时注意天气变化。 旅游指数：有阵雨，气温较高，但风较大，能缓解湿热的感觉，还是适宜旅游，您仍可陶醉于大自然的美丽风光中。 路况指数：有降水，路面潮湿，车辆易打滑，请小心驾驶。 舒适度指数：天气较热，虽然有降水，但仍然无法削弱较高气温给人们带来的暑意，这种天气会让您感到不很舒适。 空气污染指数：气象条件有利于空气污染物稀释、扩散和清除，可在室外正常活动。 紫外线指数：属中等强度紫外线辐射天气，外出时建议涂擦SPF高于15、PA+的防晒护肤品，戴帽子、太阳镜。</string>
            //<string>27℃/34℃</string>
            //<string>8月12日 多云</string> 
            //<string>南风3-4级</string> 
            //<string>1.gif</string> 
            //<string>1.gif</string> 
            //<string>28℃/34℃</string>
            //<string>8月13日 阵雨</string> 
            //<string>南风3-4级</string> 
            //<string>3.gif</string> 
            //<string>3.gif</string> 
            //<string>上海简称：沪，位置：上海地处长江三角洲前缘，东濒东海，南临杭州湾，西接江苏，浙江两省，北界长江入海，正当我国南北岸线的中部，北纬31°14′，东经121°29′。面积：总面积7823.5平方公里。人口：人口1000多万。上海丰富的人文资源、迷人的城市风貌、繁华的商业街市和欢乐的节庆活动形成了独特的都市景观。游览上海，不仅能体验到大都市中西合壁、商儒交融、八方来风的氛围，而且能感受到这个城市人流熙攘、车水马龙、灯火璀璨的活力。上海在中国现代史上占有着十分重要的地位，她是中国共产党的诞生地。许多震动中外的历史事件在这里发生，留下了众多的革命遗迹，处处为您讲述着一个个使人永不忘怀的可歌可泣的故事，成为包含民俗的人文景观和纪念地。在上海，每到秋祭，纷至沓来的人们在这里祭祀先烈、缅怀革命历史,已成为了一种风俗。大上海在中国近代历史中，曾是风起云涌可歌可泣的地方。在这里荟萃多少风云人物，散落在上海各处的不同住宅建筑，由于其主人的非同寻常，蕴含了耐人寻味的历史意义。这里曾留下许多革命先烈的足迹。瞻仰孙中山、宋庆龄、鲁迅等故居，会使您产生抚今追昔的深沉遐思，这里还有无数个达官贵人的住宅，探访一下李鸿章、蒋介石等人的公馆，可以联想起主人那段显赫的发迹史。</string>
            #endregion

            if (weatherInfoList != null && weatherInfoList.Length > 0)
            {
                tmp = "";
                foreach (string s in weatherInfoList)
                {
                    tmp += s + Entity.App.SpiderChar;
                }
                tmp = tmp.Remove(tmp.Length - 1);

                if (!File.Exists(path))
                    Helper.WriteFile(path, tmp);
            }
            return weatherInfoList;
        }
    }
}
