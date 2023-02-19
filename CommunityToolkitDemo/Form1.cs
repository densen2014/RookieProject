using CommunityToolkit.Mvvm.Messaging;

namespace CommunityToolkitDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TestConfig testConfig;
            testConfig = new TestConfig();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            Task.Run(() =>
            {
                while (!cancelTokenSource.IsCancellationRequested)
                {
                    testConfig.Name10 = DateTime.Now.ToString();
                    // Send a message from some other module
                    WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(testConfig));

                    Thread.Sleep(100);
                }

            }, token);

            // Register a message in some module
            WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage2>(this, (r, m) =>
            {
                // Handle the message here, with r being the recipient and m being the
                // input message. Using the recipient passed as input makes it so that
                // the lambda expression doesn't capture "this", improving performance.
                this.Invoke(() => this.Text = m.Value.Name10);
            });

            this.FormClosing += (s, e) =>
            {
                cancelTokenSource.Cancel();
                WeakReferenceMessenger.Default.UnregisterAll(this);
            };
        }

        private void observablePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void 消息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().Show();
        }
    }
}
