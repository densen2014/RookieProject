using System.Diagnostics;
using System.Text;

namespace FileStreams
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.Add("key", "ƫ����");
            dataGridView1.Columns.Add("hex", "Hex");
            dataGridView1.Columns.Add("text", "ASCii");
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 600;
            dataGridView1.Columns[2].Width = 400;
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.FileName = "TextFile1.txt";

            this.Text = "��ʾֻ�����500,ÿ��16�ֽ�";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //��ȡ�ļ�·��
            string filePath = "TextFile1.txt";

            var res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
            }
            dataGridView1.Rows.Clear();

            //ѭ����ȡ���ı��ļ�
            FileStream fsRead;
            //��FileStream�ļ������ļ�
            try
            {
                fsRead = new FileStream(@filePath, FileMode.Open,FileAccess.Read);
            }
            catch (Exception)
            {
                throw;
            }
            //��û�ж�ȡ���ļ����ݳ���
            long leftLength = fsRead.Length>500?500 : fsRead.Length;
            //���������ļ����ݵ��ֽ�����
            byte[] buffer = new byte[16];
            //ÿ�ζ�ȡ������ֽ���
            int maxLength = buffer.Length;
            //ÿ��ʵ�ʷ��ص��ֽ�������
            int num = 0;
            //�ļ���ʼ��ȡ��λ��
            int fileStart = 0;
            while (leftLength > 0)
            {
                //�����ļ����Ķ�ȡλ��
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
    public static class Tools {
        /// <summary>
        /// ת��Ϊ16����
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lowerCase">�Ƿ�Сд</param>
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