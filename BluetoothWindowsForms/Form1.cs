using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BluetoothWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //处理蓝牙的对象
            BluetoothClient client = new BluetoothClient();
            //获取电脑蓝牙
            BluetoothRadio radio = BluetoothRadio.PrimaryRadio;
            //设置电脑蓝牙可被搜索到
            radio.Mode = RadioMode.Connectable;
            //需要连接的蓝牙模块的唯一标识符
            BluetoothAddress blueAddress = null;//= new BluetoothAddress(new byte[] { 0x8e, 0xed, 0x10, 0xa3, 0xa8, 0xaa });
            //搜索蓝牙设备，10秒
            BluetoothDeviceInfo[] devices = client.DiscoverDevices();
            //从搜索到的所有蓝牙设备中选择需要的那个
            //BarCode Scanner HID =》蓝牙设备名称
            foreach (var item in devices)
            {
                textBox1.Text += $"{item.DeviceName} [{item.DeviceAddress}]{Environment.NewLine}";
                //根据蓝牙名字找
                if (item.DeviceName.Equals("BarCode Scanner HID"))
                {
                    Console.WriteLine(item.DeviceAddress);
                    Console.WriteLine(item.DeviceName);
                    //获得蓝牙模块的唯一标识符
                    blueAddress = item.DeviceAddress;
                    break;
                }
            }
            if (blueAddress != null)
            {
                //BluetoothService.SerialPort根本无用
                BluetoothEndPoint ep = new BluetoothEndPoint(blueAddress, Guid.Parse("00001124-0000-1000-8000-00805f9b34fb"));
                //BluetoothEndPoint ep = new BluetoothEndPoint(blueAddress, BluetoothService.SerialPort);
                client.Connect(ep);//开始配对
                if (client.Connected)
                {
                    //连接成功

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
