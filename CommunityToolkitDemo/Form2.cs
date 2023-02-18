namespace CommunityToolkitDemo;

public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();

        TestConfig testConfig;
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        this.Width = 1000;
        this.Height = 500;
        testConfig = new TestConfig();
        var btn0 = new Button();
        btn0.Width = 100;
        btn0.Height = 100;
        btn0.Text = "全禁用/启用";
        this.flowLayoutPanel1.Controls.Add(btn0);
        btn0.Click += (s, e) =>
        {
            testConfig.Enabled = !testConfig.Enabled;
            testConfig.Enabled2 = testConfig.Enabled;
        };
        var btn1 = new Button();
        btn1.Width = 100;
        btn1.Height = 100;
        btn1.Text = "1-10 禁用/启用";
        this.flowLayoutPanel1.Controls.Add(btn1);
        btn1.Click += (s, e) =>
        {
            testConfig.Enabled2 = !testConfig.Enabled2;
        };

        for (int i = 1; i <= 20; i++)
        {
            var btn = new Button();
            //btn.Enabled = false;
            btn.Width = 100;
            btn.Height = 100;
            btn.DataBindings.Add("Text", testConfig, $"name{i}");
            btn.DataBindings.Add("Enabled", testConfig, i<11?"Enabled2":"Enabled");
            SetPropValue(testConfig, $"Name{i}", btn +i.ToString());
            this.flowLayoutPanel1.Controls.Add(btn);

        }

        
        Task.Run(() =>
        {
            while (true)
            { 
                for (int i = 1; i <= 20; i++)
                {
                    try
                    {
                    var name = $"Name{i}";
                    this.Invoke (()=>SetPropValue(testConfig, name, DateTime.Now.Ticks.ToString()));
                    Thread.Sleep(100);

                    }
                    catch  
                    { 
                    }
                }
            }
        }, token);

        static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        static void SetPropValue(object src, string propName,object value)
        {
            src.GetType().GetProperty(propName).SetValue(src, value);
        }

        this.FormClosing += (s, e) =>
        {
            cancelTokenSource.Cancel();
        };
    }


}