using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunityToolkitDemo
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            // Register a message in some module
            WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, (r, m) =>
            {
                try
                {
                    this.Invoke(() => this.Text = m.Value.Name10);
                }
                catch  
                { 
                }

            });

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            TestConfig testConfig;
            testConfig = new TestConfig();
            Task.Run(() =>
            {
                while (!cancelTokenSource.IsCancellationRequested)
                {
                    testConfig.Name10 ="From3=>" + DateTime.Now.ToString();
                    // Send a message from some other module
                    WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage2(testConfig));

                    Thread.Sleep(100);
                }

            }, token);


            this.FormClosing += (s, e) =>
            {
                cancelTokenSource.Cancel();
                WeakReferenceMessenger.Default.UnregisterAll(this);
            };

        }
    }
}
