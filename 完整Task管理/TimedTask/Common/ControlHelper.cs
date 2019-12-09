// 版权所有：
// 文 件  名：UIControlHelper.cs
// 功能描述：
// 创建标识：Seven Song(m.sh.lin0328@163.com) 2014/1/19 21:48:29
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace TimedTask
{
    public class ControlHelper
    {
        private static ControlHelper _instance;
        private static object _lock = new object();//使用static object作为互斥资源
        private static readonly object _obj = new object();

        #region 单例
        /// <summary>
        /// 
        /// </summary>
        private ControlHelper() { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static ControlHelper GetInstense()
        {
            if (_instance == null)
            {
                lock (_obj)
                {
                    if (_instance == null)
                    {
                        _instance = new ControlHelper();
                    }
                }
            }
            return _instance;
        }
        /// <summary>
        /// 返回唯一实例
        /// </summary>
        public static ControlHelper Instance
        {
            get
            {
                return GetInstense();
            }
        }
        #endregion

        #region 设置控件属性

        /// <summary>
        /// 设置控件文本
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="text">文本</param>
        public void SetControlText(System.Windows.Controls.Control control, string text)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (control is System.Windows.Controls.Label)
                {
                    System.Windows.Controls.Label lbl = (System.Windows.Controls.Label)control;
                    lbl.Content = text;
                }
                else if (control is System.Windows.Controls.TextBox)
                {
                    System.Windows.Controls.TextBox txt = (System.Windows.Controls.TextBox)control;
                    txt.Text = text;
                }
            }));
        }

        /// <summary>
        /// 设置Boder背景图片
        /// </summary>
        /// <param name="boder"></param>
        /// <param name="imgPath"></param>
        public void SetBackground(System.Windows.Controls.Border boder, string imgPath)
        {
            if (String.IsNullOrEmpty(imgPath) || !File.Exists(imgPath))
                return;

            try
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imgPath));
                brush.Stretch = Stretch.Fill;
                boder.Background = brush;
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region DataGrid控件查找

        /// <summary>
        /// 从DataGrid中找到指定控件
        /// 用法：CheckBox chk_All = UIControlHelper.FindCellControl<CheckBox>(dg_Main, "chk_All", 0, 0);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dgId">DataGrid 名</param>
        /// <param name="controlName">控件名</param>
        /// <param name="rowIndex">行</param>
        /// <param name="columnIndex">列</param>
        /// <returns></returns>
        public T FindCellControl<T>(DataGrid dgId, string controlName, int rowIndex, int columnIndex) where T : Visual
        {
            try
            {
                DataGridCell cell = GetCell(dgId, rowIndex, columnIndex);
                return FindVisualChildByName<T>(cell, controlName) as T;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="controlName">控件名</param>
        /// <returns></returns>
        public T FindVisualChildByName<T>(Visual parent, string controlName) where T : Visual
        {
            /* 查找XAML中的控件
                   Window window = null;
            using (FileStream fs = new FileStream("MyWindow.xaml", FileMode.Open, FileAccess.Read))
            {
                window = (Window)System.Windows.Markup.XamlReader.Load(fs);
            }
            System.Windows.Controls.Button btnOk = (System.Windows.Controls.Button)window.FindName("btnOk");
             */
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i) as Visual;
                    string name = child.GetValue(Control.NameProperty) as string;
                    if (controlName == name)
                    {
                        return child as T;
                    }
                    else
                    {
                        T result = FindVisualChildByName<T>(child, controlName);
                        if (result != null)
                            return result;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 获得DataGridCell 
        /// </summary>
        /// <param name="dgId">DataGrid控件</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <returns></returns>
        public DataGridCell GetCell(DataGrid dgId, int rowIndex, int columnIndex)
        {
            DataGridRow dgRow = GetRow(dgId, rowIndex);
            if (dgRow != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(dgRow);
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                if (cell == null)
                {
                    dgId.ScrollIntoView(dgRow, dgId.Columns[columnIndex]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                }
                return cell;
            }
            return null;
        }
        /// <summary>
        /// 获得DataGridRow
        /// </summary>
        /// <param name="dgId">dgId">DataGrid控件</param>
        /// <param name="columnIndex">列索引</param>
        /// <returns></returns>
        public DataGridRow GetRow(DataGrid dgId, int columnIndex)
        {
            if (columnIndex < 0)
                return null;

            DataGridRow row = null;
            try
            {
                row = (DataGridRow)dgId.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                if (row == null)
                {
                    dgId.UpdateLayout();
                    dgId.ScrollIntoView(dgId.Items[columnIndex]);
                    row = (DataGridRow)dgId.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return row;
        }
        /// <summary>
        /// 获取可视树下子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        /// <summary>
        /// 返回DataGrid列索引
        /// </summary>
        /// <param name="grId"></param>
        /// <param name="Caption">列头名</param>
        /// <returns></returns>
        public int GetColumnIndexByCaption(DataGrid dgId, string Caption)
        {
            for (int i = 0; i < dgId.Columns.Count; i++)
            {
                if (dgId.Columns[i].Header == Caption)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region 加载XAML

        /// <summary>
        /// 从外部文件加载
        /// </summary>
        /// <param name="path">绝对路径</param>
        /// <param name="ctr"></param>
        public void LoadXamlByFile(string path, System.Windows.Controls.StackPanel ctr)
        {
            try
            {
                XmlTextReader xmlreader = new XmlTextReader(path);
                UIElement obj = XamlReader.Load(xmlreader) as UIElement;
                ctr.Children.Add((UIElement)obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 从资源文件中加载
        /// </summary>
        /// <param name="path">资源路径 相对，如：/view/a.xaml</param>
        /// <param name="ctr"></param>
        public void LoadXamlByResource(string path, System.Windows.Controls.StackPanel ctr)
        {
            //Build Action = Resource,Do not Copy,无相应cs文件
            Uri uri = new Uri(path, UriKind.Relative);
            Stream stream = Application.GetResourceStream(uri).Stream;
            //FrameworkElement继承自UIElement
            FrameworkElement obj = XamlReader.Load(stream) as FrameworkElement;
            ctr.Children.Add(obj);
        }
        #endregion

        #region ComBox 添加项
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="dic"></param>
        public void CboAdd(ComboBox cbo, Dictionary<string, string> dic)
        {
            if (dic == null || dic.Count == 0)
                return;

            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Value";

            MyItem mi = new MyItem();
            cbo.Items.Clear();

            foreach (KeyValuePair<string, string> kvp in dic)
            {
                mi = new MyItem();
                mi.Name = kvp.Key;
                mi.Value = kvp.Value;
                cbo.Items.Add(mi);
            }
        }
        /// <summary>
        /// ComBox 添加项
        /// </summary>
        private struct MyItem
        {
            public string Name { set; get; }
            public string Value { set; get; }
        }
        #endregion
    }
}
