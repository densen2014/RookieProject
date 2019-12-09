using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace TimedTask.Common
{
    /// <summary>
    /// Description: Hook Helper类，Alt+Ctrl+Del是系统底层热键，暂时无法屏蔽
    /// Author: mashanlin
    /// Create DateTime: 2014-5-17 12:21
    /// </summary>
    public class HookHelper
    {
        /// 声明回调函数委托  
        /// </summary>  
        /// <param name="nCode"></param>  
        /// <param name="wParam"></param>  
        /// <param name="lParam"></param>  
        /// <returns></returns>  
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        /// <summary>  
        /// 委托实例  
        /// </summary>  
        HookProc KeyboardHookProcedure;

        /// <summary>  
        /// 键盘钩子句柄  
        /// </summary>  
        static int hKeyboardHook = 0;

        //装置钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //卸下钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //获取某个进程的句柄函数  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>  
        /// 普通按键消息  
        /// </summary>  
        private const int WM_KEYDOWN = 0x100;
        /// <summary>  
        /// 系统按键消息  
        /// </summary>  
        private const int WM_SYSKEYDOWN = 0x104;

        //鼠标常量   
        public const int WH_KEYBOARD_LL = 13;

        //声明键盘钩子的封送结构类型   
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode; //表示一个在1到254间的虚似键盘码   
            public int scanCode; //表示硬件扫描码   
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        //安装钩子
        public bool HookStart()
        {
            //启动键盘钩子   
            if (hKeyboardHook == 0)
            {
                //实例化委托  
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                Process curProcess = Process.GetCurrentProcess();
                ProcessModule curModule = curProcess.MainModule;
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, GetModuleHandle(curModule.ModuleName), 0);
            }
            if (hKeyboardHook == 1)
                return true;

            return false;
        }
        /// <summary>  
        /// 截取全局按键，屏蔽热键设置
        /// </summary>  
        /// <param name="nCode"></param>  
        /// <param name="wParam"></param>  
        /// <param name="lParam"></param>  
        /// <returns></returns>  
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
            {
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                System.Windows.Forms.Keys currentKey = (System.Windows.Forms.Keys)MyKeyboardHookStruct.vkCode;

                if (
                    (currentKey == Keys.F4 && (int)Control.ModifierKeys == (int)Keys.Alt)//Alt+F4
                    || (currentKey == Keys.Tab && (int)Control.ModifierKeys == (int)Keys.Alt)//Alt+Tab
                    || (currentKey == Keys.LWin && (int)Control.ModifierKeys == (int)Keys.R)//徽标+R
                    )
                {
                    //MessageBox.Show("你按下了Ctrl+1");
                    //return为了屏蔽原来的按键，如果去掉，则原来的按键和新的按键都会模拟按。  
                    return 1;
                }
            }
            return 0;
        }

        /// <summary>  
        /// 停止键盘钩子  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        public void HookStop()
        {
            bool retKeyboard = true;

            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
            //如果卸下钩子失败   
            if (!(retKeyboard))
                throw new Exception("卸下钩子失败！");
        }
    }
}
