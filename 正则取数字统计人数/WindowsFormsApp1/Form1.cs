using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("key","项目");
            dataGridView1.Columns.Add("value","合计");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dataGridView1.Rows.Clear();
            var f2 = new Form2();
            f2.ShowDialog();
            f2.Dispose();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strs = $"{textBox1.Text}\n{textBox2.Text}\n{textBox3.Text}\n{textBox4.Text}\n{textBox5.Text}";

            var list = new Dictionary<string, int>();
            foreach (var str in strs.Replace("\r", "").Split('\n').Where(a => a.Length > 0))
            {
                var result = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", ""));
                var key = str.Replace(result.ToString(), "").Replace("条", "");
                if (list.ContainsKey(key))
                {
                    list[key] += result;
                }
                else
                {
                    list.Add(key, result);
                }

            }

          foreach (var item in list.OrderBy(a => a.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
                dataGridView1.Rows.Add(item.Key, item.Value);
            }
        }
    }
}
