using Newtonsoft.Json;
using System.Diagnostics;
using static WinFormsJsonTreeView.Rootobject;

namespace WinFormsJsonTreeView
{
    public partial class Form1 : Form
    {
        string json = @"{""BigIntSupported"":995815895020119788889,""date"":""20180322"",""message"":""Success !"",""status"":200,""city"":""����"",""count"":632,""data"":{""shidu"":""34%"",""pm25"":73,""pm10"":91,""quality"":""��"",""wendu"":""5"",""ganmao"":""������������ȺӦ���ٻ���"",""yesterday"":{""date"":""21��������"",""sunrise"":""06:19"",""high"":""���� 11.0��"",""low"":""���� 1.0��"",""sunset"":""18:26"",""aqi"":85,""fx"":""�Ϸ�"",""fl"":""<3��"",""type"":""����"",""notice"":""����֮�䣬��������������""},""forecast"":[{""date"":""22��������"",""sunrise"":""06:17"",""high"":""���� 17.0��"",""low"":""���� 1.0��"",""sunset"":""18:27"",""aqi"":98,""fx"":""���Ϸ�"",""fl"":""<3��"",""type"":""��"",""notice"":""Ը��ӵ�б��������ĵ�����""},{""date"":""23��������"",""sunrise"":""06:16"",""high"":""���� 18.0��"",""low"":""���� 5.0��"",""sunset"":""18:28"",""aqi"":118,""fx"":""�޳�������"",""fl"":""<3��"",""type"":""����"",""notice"":""����֮�䣬��������������""}]}}";

        Rootobject root;
        Forecast selectItem = new Forecast();

        public Form1()
        {
            InitializeComponent();

            root = JsonConvert.DeserializeObject<Rootobject>(json);
            var foo = new FooNode(root.data);
            treeView1.Nodes.Add(foo);
            treeView1.ExpandAll();

            treeView1.AfterSelect += ((x, s) =>
            {
                if (s.Node is BarNode)
                {
                    selectItem = ((BarNode)s.Node).Bar;
                    dataGridView1.DataSource = new List<Forecast> { selectItem };
                }
                else if (s.Node is FooNode)
                {
                    dataGridView1.DataSource = new List<Data> { ((FooNode)s.Node).Foo };
                }
            });




        }

        private class FooNode : TreeNode
        {
            public FooNode(Data foo)
            {
                this.Text = foo.quality;
                this.Foo = foo;
                this.Nodes.Add($"{nameof(foo.shidu)} : {foo.shidu}");
                this.Nodes.Add($"{nameof(foo.wendu)} : {foo.wendu}");
                this.Nodes.Add($"{nameof(foo.ganmao)} : {foo.ganmao}");
                this.Foo.forecast.ForEach(x => this.Nodes.Add(new BarNode(x)));

            }

            public Data Foo
            {
                get;
                private set;
            }
        }

        private class BarNode : TreeNode
        {
            public BarNode(Forecast bar)
            {
                this.Text = bar.date;
                this.Bar = bar;
                this.Nodes.Add($"{nameof(bar.date)} : {bar.date}");
                this.Nodes.Add($"{nameof(bar.sunrise)} : {bar.sunrise}");
                this.Nodes.Add($"{nameof(bar.high)} : {bar.high}");
                this.Nodes.Add($"{nameof(bar.low)} : {bar.low}");
                this.Nodes.Add($"{nameof(bar.sunset)} : {bar.sunset}");
                this.Nodes.Add($"{nameof(bar.aqi)} : {bar.aqi}");
                this.Nodes.Add($"{nameof(bar.fx)} : {bar.fx}");
                this.Nodes.Add($"{nameof(bar.fl)} : {bar.fl}");
                this.Nodes.Add($"{nameof(bar.type)} : {bar.type}");
                this.Nodes.Add($"{nameof(bar.notice)} : {bar.notice}");
            }

            public Forecast Bar
            {
                get;
                private set;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var _json = JsonConvert.SerializeObject(root, setting2);
            Debug.WriteLine(_json);
            textBox1.Text = _json;
            var foo = new FooNode(root.data);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(foo);
            treeView1.ExpandAll();
        }

        readonly JsonSerializerSettings setting2 = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

    }






}