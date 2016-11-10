﻿using System;

namespace 报社与读者__EventArgs
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NewspaperOffice xinHua = new NewspaperOffice("新华日报");
            NewspaperOffice wuxiShang = new NewspaperOffice("无锡商报");

            NewspaperReader xiaoLiu = new NewspaperReader("小刘");
            NewspaperReader xiaoWang = new NewspaperReader("小王");

            xiaoLiu.SubscribeNewspaper(xinHua);
            xiaoWang.SubscribeNewspaper(xinHua);
            xiaoWang.SubscribeNewspaper(wuxiShang);

            xinHua.PrintNewspaper("2013-08-09日报，内容..........");
            wuxiShang.PrintNewspaper("2013-08-10,内容..........");

            xiaoWang.UnSubscribeNewspaper(xinHua);
            xinHua.PrintNewspaper("2013-08-11日报，内容..........");
            wuxiShang.PrintNewspaper("2013-08-12,内容..........");

            Console.ReadKey();
        }
    }
}