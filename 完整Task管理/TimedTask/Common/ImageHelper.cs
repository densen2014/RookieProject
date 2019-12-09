using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TaskScheduler.Core
{
    public class ImageHelper
    {
        /// <summary>
        /// 解析字节数组成图片
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;
            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }


        /// <summary>
        /// 图片数据解析成字节流数组(用于存储到数据库)
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] byteArray = null;
            try
            {
                Stream sMarket = bmp.StreamSource;
                if (sMarket != null && sMarket.Length > 0)
                {
                    sMarket.Position = 0;
                    using (BinaryReader br = new BinaryReader(sMarket))
                    {
                        byteArray = br.ReadBytes((int)sMarket.Length);
                    }
                }
            }
            catch
            {
            }
            return byteArray;
        }

        /// <summary>
        /// 根据图片的路径解析成图片资源
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToByteArray(String filePath)
        {

            byte[] byteArray = null;
            if (File.Exists(filePath))
                byteArray = File.ReadAllBytes(filePath);
            return byteArray;
        }

        /// <summary>
        /// 根据图片的相对路径 返回 BitmapImage对象的实例化
        /// </summary>
        /// <param name="imgPath">图片的相对路径(如:@"/images/star.png")</param>
        /// <returns></returns>
        public static BitmapImage GetBitmapImage(string imgPath)
        {
            try
            {
                if (!imgPath.StartsWith("/"))
                {
                    imgPath = "/" + imgPath;
                }
                return new BitmapImage(new Uri("Pack://application:,,," + imgPath));
            }
            catch
            {
                return EmptyImageSource;
            }
        }

        /// <summary>
        /// 根据图片的相对路径 获取Image对象
        /// </summary>
        /// <param name="imgPath">图片的相对路径(如:@"/images/star.png")</param>
        /// <returns></returns>
        public static Image GetImage(string imgPath)
        {
            if (File.Exists(imgPath))
            {
                Image im = new Image();
                im.Source = GetBitmapImage(imgPath);
                return im;
            }
            else
                return null;
        }

        /// <summary>
        /// 根据图片的相对路径 获取ImageBrush对象 (此对象资源可以直接用于绑定控件的Background属性)
        /// </summary>
        /// <param name="imgPath">图片的相对路径(如:@"/images/star.png")</param>
        /// <returns></returns>
        public static ImageBrush GetImageBrush(string imgPath)
        {
            if (File.Exists(imgPath))
            {
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = GetBitmapImage(imgPath);
                return ib;
            }
            else
                return null;
        }
    }
}
