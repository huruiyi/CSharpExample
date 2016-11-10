﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 报社与读者_Event
{
    public class NewspaperReader
    {
        public void SubscribeNewspaper(NewspaperOffice office)
        {
            office.OnNewspaperPrint += Read;
            Console.WriteLine("{0}订阅了{1}报纸", Name, office.Name);
        }

        public void UnSubscribeNewspaper(NewspaperOffice office)
        {
            office.OnNewspaperPrint -= Read;
            Console.WriteLine("{0},退订了{1}报纸", Name, office.Name);
        }

        public void Read(string content)
        {
            Console.WriteLine("{0}正在读报纸,内容为：{1}", Name, content);
        }

        public string Name { get; set; }

        public NewspaperReader(string name)
        {
            Name = name;
        }
    }
}