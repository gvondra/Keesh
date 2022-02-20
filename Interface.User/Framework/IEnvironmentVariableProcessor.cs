﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Framework
{
    public interface IEnvironmentVariableProcessor
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}
