﻿using System;
using System.Windows.Forms;

namespace 登陆窗体
{
    internal static class Program
    {
        public static bool b = false;

        [STAThread]
        private static void Main()
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            if (b == true)
            {
                Application.Run(new 主窗体());
            }
        }
    }
}