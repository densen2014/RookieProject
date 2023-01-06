using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.DataAnnotationsValidation
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var list = new List<SampleModel>()
            {
                new SampleModel(){
                    Name ="Some long name",
                    Price = -1,
                    Description = "something",
                    Url = "www.site.com"
                }
            };
            this.sampleModelBindingSource.DataSource = list;
        }
    }
}
