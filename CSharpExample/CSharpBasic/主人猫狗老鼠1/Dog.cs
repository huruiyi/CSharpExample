﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 主人猫狗老鼠1
{
    internal class Dog
    {
        public Human Master { get; set; }

        public string Name { get; set; }

        public Dog(Human master, string name)
        {
            Name = name;
            Master = master;
            master.OnSpeak += OnMasterSpeak;
        }

        public void OnMasterSpeak(string content)
        {
            if (content == "Bye Bye")
            {
                Console.WriteLine("{0}狗叫了声：汪汪", Name);
            }
        }
    }
}