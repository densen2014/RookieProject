using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; 
using System.IO;

namespace FileStreams461
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.Add("key", "偏移量");
            dataGridView1.Columns.Add("hex", "Hex");
            dataGridView1.Columns.Add("text", "ASCii");
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 700;
            dataGridView1.Columns[2].Width = 400;
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.FileName = "TextFile1.txt";

            this.Text = "演示只打开最大500,每次16字节";
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取文件路径
            string filePath = "TextFile1.txt";

            var res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
            }
            dataGridView1.Rows.Clear();

            //循环读取大文本文件
            FileStream fsRead;
            //用FileStream文件流打开文件
            try
            {
                fsRead = new FileStream(@filePath, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                throw;
            }
            //还没有读取的文件内容长度
            long leftLength = fsRead.Length > 500 ? 500 : fsRead.Length;
            //创建接收文件内容的字节数组
            byte[] buffer = new byte[16];
            //每次读取的最大字节数
            int maxLength = buffer.Length;
            //每次实际返回的字节数长度
            int num = 0;
            //文件开始读取的位置
            int fileStart = 0;
            while (leftLength > 0)
            {
                //设置文件流的读取位置
                fsRead.Position = fileStart;
                if (leftLength < maxLength)
                {
                    num = fsRead.Read(buffer, 0, Convert.ToInt32(leftLength));
                }
                else
                {
                    num = fsRead.Read(buffer, 0, maxLength);
                }
                if (num == 0)
                {
                    break;
                }
                fileStart += num;
                leftLength -= num;
                Debug.WriteLine(Encoding.Default.GetString(buffer));
                var split = buffer.ToHex().Split('\t');
                dataGridView1.Rows.Add(fileStart, buffer.ToHex(), Encoding.Default.GetString(buffer));

            }
            fsRead.Close();

        }
    }
    public static class Tools
    {
        /// <summary>
        /// 转换为16进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string ToHex(this byte[] bytes, bool lowerCase = false)
        {
            if (bytes == null)
                return null;

            var result = new StringBuilder();
            var format = lowerCase ? "x2" : "X2";
            int j = 0;
            for (var i = 0; i < bytes.Length; i++)
            {
                j++;
                result.Append(bytes[i].ToString(format) + (j == 4 ? "   " : " "));
                j = j == 4 ? 0 : j;
            }

            return result.ToString();
        }

    }

}
