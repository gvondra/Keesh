﻿using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Framework
{
    public interface ICompanyOverviewFactory
    {
        Task<CompanyOverview> Get(string symbol, string apiKey);
    }
}
