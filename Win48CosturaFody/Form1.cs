using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win48CosturaFody
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = CosturaFodyDLL1.Class1.ShowMyVer() + "   " + CosturaFodyDLL1.Class1.ShowMyVer2();
            var x = GetTypes("CosturaFodyDLL1");
        }



        /// <summary>  
        /// 获取一个命名空间下的所有类  
        /// </summary>
        /// <param name="amespaceName"></param>
        /// <param name="assemblyString"></param>   
        /// <returns></returns>  
        public static List<Type> GetTypes(string amespaceName = "AME.Models.Entity.", string assemblyString = null) //"AME.Models.Entity.", "AME.API"
        {
            List<Type> lt = new List<Type>();
            try
            {
                var lists = assemblyString == null ?
                    System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(a => a.IsClass == true && a.FullName.StartsWith(amespaceName)).ToList() :
                    System.Reflection.Assembly.Load(assemblyString).GetTypes().Where(a => a.IsClass == true && a.FullName.StartsWith(amespaceName)).ToList();
                lt.AddRange(lists);
            }
            catch { }
            return lt;
        }
    }
}
