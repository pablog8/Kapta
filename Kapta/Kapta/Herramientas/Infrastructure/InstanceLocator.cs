﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kapta.Herramientas.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
