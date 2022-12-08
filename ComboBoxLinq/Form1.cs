namespace ComboBoxLinq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var items = textBox1.Text.Split(Environment.NewLine);
            var ds = items.OrderBy(a => a.Length).ThenBy(a => a).ToList();
            comboBox1.DataSource = ds;
        }
    }
}