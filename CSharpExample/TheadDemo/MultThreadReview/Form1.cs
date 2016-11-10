﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MultThreadReview
{
    public partial class Form1 : Form
    {
        private readonly List<Label> lbList = new List<Label>();

        public bool IsCreate = false;

        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsCreate)
            {
                IsCreate = false;
                btnStart.Text = "开始";
            }
            else
            {
                this.btnStart.Text = "结束";
                IsCreate = true;

                //启动另外一个线程去 改变 lable 的值
                new Thread(() =>
                {
                    Random random = new Random();
                    while (IsCreate)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            //闭包导致了 i的作用域变大了。出现了一些异常的情况
                            //Thread thead = new Thread((a) => SetText(a),i);
                            lbList[i].Text = random.Next(1, 10).ToString();
                        }
                        //让线程停顿一下
                        Thread.Sleep(300);
                    }
                }).Start();
            }
        }

        public void SetText(int i)
        {
            lbList[i].Text = "ddd";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                Label label = new Label
                {
                    Text = i.ToString(),
                    AutoSize = true,
                    Location = new Point(50 * i + 50, 50)
                };

                lbList.Add(label);

                this.Controls.Add(label);
            }
        }
    }
}