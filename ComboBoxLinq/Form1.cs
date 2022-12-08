namespace ComboBoxLinq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1_Click(null, null);
        }

        private void button1_Click(object? sender, EventArgs? e)
        {
            var items = textBox1.Text.Split(Environment.NewLine);
            var newItems = new List<string>();
            //����/�����ַ���
            var dgNum = items.GroupBy(a => !a.IsNumeric()).ToList();
            foreach (var item1 in dgNum)
            {
                //���ȷ���
                var dgLen = item1.GroupBy(a => a.Length).ToList();
                foreach (var item2 in dgLen)
                {
                    //���һλ��ĸ����
                    var dgLast = item2.GroupBy(a => a.Last()).ToList();
                    dgLast.ForEach(newItems.AddRange);
                }
            }
            //��Ȼ����
            //var ds = items.OrderBy(a => a.Length).ThenBy(a => a).ToList();
            comboBox1.DataSource = newItems;
        }

        private void ����1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = @"OFF
69.2
67.0
D023N
D024I
D025N";
            button1_Click(null, null);

        }

        private void ����2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = @"OFF
69.2
67.0
D023N
D024I
D025N
D0214I
123
33.2
D043N
678";
            button1_Click(null, null);
        }

        private void ����3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = @"OFF
69.2
67.0
D023N
D024I
213.02
ON
D025N
D0214I
123
33.2
D043N
678";
            button1_Click(null, null);
        }
    }
    public static class StringExt
    {
        public static bool IsNumeric(this string text) => double.TryParse(text, out _);
    }
}